using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fn_Set_Primary_image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
CREATE OR REPLACE FUNCTION public.set_primary_image(
	p_image_id integer)
    RETURNS void
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    v_product_id INTEGER;
BEGIN
    -- Step 1: Find the product_id for the given image_id
    SELECT product_id INTO v_product_id
    FROM public.product_images
    WHERE id = p_image_id;

    -- If the product_id is not found, raise an exception
    IF v_product_id IS NULL THEN
        RAISE EXCEPTION 'No product found for image_id %', p_image_id;
    END IF;

    -- Step 2: Set all images for this product to not primary
    UPDATE public.product_images
    SET is_primary = false
    WHERE product_id = v_product_id;
    
    -- Step 3: Set the specific image as primary
    UPDATE public.product_images
    SET is_primary = true
    WHERE id = p_image_id;

    -- Optionally, raise a notice to confirm success
    RAISE NOTICE 'Image % set as primary for product %', p_image_id, v_product_id;

EXCEPTION
    WHEN OTHERS THEN
        -- Handle exceptions (optional)
        RAISE EXCEPTION 'Error setting primary image for image_id %: %', p_image_id, SQLERRM;
END;
$BODY$;

ALTER FUNCTION public.set_primary_image(integer)
    OWNER TO postgres;
"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
