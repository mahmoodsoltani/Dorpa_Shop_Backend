using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Insert_ProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
CREATE OR REPLACE FUNCTION public.insert_product_image(
	p_product_id integer,
	p_image_url text,
	p_alt_text text,
	p_is_primary boolean)
    RETURNS integer
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    v_image_id INTEGER;
BEGIN
    -- Step 1: If the image is marked as primary, set all other images for this product to non-primary
    IF p_is_primary THEN
        UPDATE public.product_images
        SET is_primary = false
        WHERE product_id = p_product_id;
    END IF;

    -- Step 2: Insert the new image
    INSERT INTO public.product_images (product_id, image_url, alt_text, is_primary, create_date, update_date)
    VALUES (p_product_id, p_image_url, p_alt_text, p_is_primary, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
    RETURNING id INTO v_image_id;

    -- Step 3: Return the newly inserted image ID
    RETURN v_image_id;

EXCEPTION
    WHEN OTHERS THEN
        -- Raise an error in case of failure
        RAISE EXCEPTION 'Error inserting product image: %', SQLERRM;
        RETURN NULL;
END;
$BODY$;

ALTER FUNCTION public.insert_product_image(integer, text, text, boolean)
    OWNER TO postgres;
"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
