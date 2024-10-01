using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fn_Insert_Update_Cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"-- FUNCTION: public.insert_or_update_cart(integer, integer, integer)

-- DROP FUNCTION IF EXISTS public.insert_or_update_cart(integer, integer, integer);

CREATE OR REPLACE FUNCTION public.insert_or_update_cart(
	p_user_id integer,
	p_product_id integer,
	p_quantity integer)
    RETURNS integer
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    existing_quantity INT;
    stock_quantity INT;
	    v_cart_detail_id INT;

BEGIN
    -- Check the current stock for the product variant
    SELECT stock INTO stock_quantity
    FROM products
    WHERE id = p_product_id;
    
    IF stock_quantity IS NULL THEN
        RAISE EXCEPTION 'Product  % does not exist.', p_product_id;
    END IF;

    -- Ensure enough stock is available
    IF p_quantity > stock_quantity THEN
        RAISE EXCEPTION 'Not enough stock for product  %. Available: %, Requested: %', p_product_id, stock_quantity, p_quantity;
    END IF;

    -- Check if the product already exists in the cart
    SELECT quantity INTO existing_quantity
    FROM cart_details
    WHERE user_id = p_user_id
    AND product_id = p_product_id;

    IF FOUND THEN
	 IF p_quantity+ existing_quantity > stock_quantity THEN
        RAISE EXCEPTION 'Not enough stock for product variant %. Available: %, Requested: %', p_product_id, stock_quantity, p_quantity+ existing_quantity;
    END IF;
        -- Update existing cart item
        UPDATE cart_details
        SET quantity = existing_quantity + p_quantity
        WHERE user_id = p_user_id
        AND product_id = p_product_id
		RETURNING id INTO v_cart_detail_id;
    ELSE
        -- Insert new cart item
        INSERT INTO cart_details (user_id, product_id, quantity)
        VALUES (p_user_id, p_product_id, p_quantity) 
		RETURNING id INTO v_cart_detail_id;
    END IF;

	
        RETURN v_cart_detail_id;

END;
$BODY$;

ALTER FUNCTION public.insert_or_update_cart(integer, integer, integer)
    OWNER TO postgres;
"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
