using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Insert_Product_WithImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql(
           @"
CREATE OR REPLACE FUNCTION public.insert_product_with_images(
	p_name text,
	p_description text,
	p_brand_id integer,
	p_category_id integer,
	p_size_id integer,
	p_price numeric,
	p_stock numeric,
	p_color_id integer,
	p_images jsonb)
    RETURNS integer
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    v_product_id INTEGER;
BEGIN
    -- Start a transaction
    BEGIN
        -- Insert the product and get its ID
        INSERT INTO public.products (name, description, brand_id, category_id, size_id,color_id, price,stock)
        VALUES (p_name, p_description, p_brand_id, p_category_id, p_size_id,p_color_id, p_price,p_stock)
        RETURNING id INTO v_product_id;

        -- Insert the images
        INSERT INTO public.product_images (product_id, image_url, is_primary, alt_text)
        SELECT 
            v_product_id,
            image_data->>'ImageUrl',
            (image_data->>'IsPrimary')::BOOLEAN,
            image_data->>'AltText'
        FROM 
            jsonb_array_elements(p_images) AS image_data;
  RETURN v_product_id;
    END;
END;
$BODY$;

ALTER FUNCTION public.insert_product_with_images(text, text, integer, integer, integer, numeric, numeric, integer, jsonb)
    OWNER TO postgres;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}
