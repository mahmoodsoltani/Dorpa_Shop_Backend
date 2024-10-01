using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fn_Update_ProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
CREATE OR REPLACE FUNCTION public.update_product_image(
	p_image_id integer,
	p_image_url text,
	p_alt_text text,
	p_is_primary boolean)
    RETURNS boolean
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    v_product_id INTEGER;
BEGIN
    -- Step 1: Find the product_id based on the image_id
    SELECT product_id INTO v_product_id
    FROM public.product_images
    WHERE id = p_image_id;

    -- If the product_id is not found, raise an exception
    IF v_product_id IS NULL THEN
        RAISE EXCEPTION 'No product found for image_id %', p_image_id;
    END IF;

    -- Step 2: Update the image_url and alt_text for the given image_id
    UPDATE public.product_images
    SET 
        image_url = p_image_url,
        alt_text = p_alt_text,
        update_date = CURRENT_TIMESTAMP
    WHERE 
        id = p_image_id;

    -- Step 3: Set the image as primary if p_is_primary is true
    IF p_is_primary THEN
        -- Step 3a: Set all other images for this product to not primary
        UPDATE public.product_images
        SET is_primary = false
        WHERE product_id = v_product_id;

        -- Step 3b: Set the given image as primary
        UPDATE public.product_images
        SET is_primary = true
        WHERE id = p_image_id;
    END IF;

    -- Optional success message
    return true;

EXCEPTION
    WHEN OTHERS THEN
        -- Handle errors if something goes wrong
        RAISE EXCEPTION 'Error updating image %: %', p_image_id, SQLERRM;
END;
$BODY$;

ALTER FUNCTION public.update_product_image(integer, text, text, boolean)
    OWNER TO postgres;
"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
