using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fn_Cart_Checkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
            

CREATE OR REPLACE FUNCTION public.cart_checkout(
	p_user_id integer)
    RETURNS integer
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
            DECLARE
                new_order_id INT;
                details RECORD;
            BEGIN
                INSERT INTO orders (user_id)
                VALUES (p_user_id)
                RETURNING id INTO new_order_id;

                IF NOT EXISTS (
                    SELECT 1 
                    FROM cart_details 
                    WHERE user_id = p_user_id
                ) THEN
                    RAISE EXCEPTION 'No cart Details found for user with ID %', p_user_id;
                END IF;

                FOR details IN
                    SELECT ci.product_id, ci.quantity
                    FROM cart_details ci
                    WHERE ci.user_id = p_user_id
                LOOP
                    IF NOT EXISTS (
                        SELECT 1
                        FROM products p
                        WHERE p.id = details.product_id
                        AND p.stock >= details.quantity
                    ) THEN
                        RAISE EXCEPTION 'Insufficient stock for product ID %', details.product_id;
                    END IF;

                    INSERT INTO order_details (price, quantity, order_id, product_id)
                    SELECT 
                        p.price,
						ROUND(sum(p.price* (100-COALESCE(d.discount_percentage, 0))/100.0 ),2),
                        details.quantity,
                        new_order_id,
                        details.product_id
                    FROM products p LEFT JOIN discounts d ON d.product_id = p.id
                    WHERE p.id = details.product_id;

                    UPDATE product 
                    SET stock = stock - details.quantity
                    WHERE id = details.productid;
                END LOOP;

                DELETE FROM cart_details
                WHERE user_id = p_user_id;
				UPDATE Orders SET total = 
				(Select ROUND(sum(p.price* (100-COALESCE(d.discount_percentage, 0))/100.0 *od.quantity),2)
			
				FROM products p 
				JOIN order_details od ON od.product_id = p.id
				JOIN orders o ON o.id = od.order_id
				LEFT JOIN discounts d ON d.product_id = p.id
				WHERE od.order_id = new_order_id
				AND o.order_date>= d.start_date AND o.order_date<= d.end_date)
				AND
				discount = 
				(Select ROUND(sum(p.price* (COALESCE(d.discount_percentage, 0))/100.0 *od.quantity),2)
			
				FROM products p 
				JOIN order_details od ON od.product_id = p.id
				JOIN orders o ON o.id = od.order_id
				LEFT JOIN discounts d ON d.product_id = p.id
				WHERE od.order_id = new_order_id
				AND o.order_date>= d.start_date AND o.order_date<= d.end_date)
				WHERE id = new_order_id;
				return new_order_id;

            EXCEPTION
                WHEN OTHERS THEN
                    RAISE;
            END;
			
$BODY$;

ALTER FUNCTION public.cart_checkout(integer)
    OWNER TO postgres;

            "
            );
        }
    }
}
