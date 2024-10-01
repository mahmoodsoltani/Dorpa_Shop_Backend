
  ------------------------------------
  CREATE
  OR REPLACE FUNCTION public.set_primary_image(p_image_id integer) RETURNS void LANGUAGE 'plpgsql' COST 100 VOLATILE PARALLEL UNSAFE AS $ $ DECLARE v_product_id INTEGER;

BEGIN -- Step 1: Find the product_id for the given image_id
SELECT
  product_id INTO v_product_id
FROM
  public.product_images
WHERE
  id = p_image_id;

-- If the product_id is not found, raise an exception
IF v_product_id IS NULL THEN RAISE EXCEPTION 'No product found for image_id %',
p_image_id;

END IF;

-- Step 2: Set all images for this product to not primary
UPDATE
  public.product_images
SET
  is_primary = false
WHERE
  product_id = v_product_id;

-- Step 3: Set the specific image as primary
UPDATE
  public.product_images
SET
  is_primary = true
WHERE
  id = p_image_id;

-- Optionally, raise a notice to confirm success
RAISE NOTICE 'Image % set as primary for product %',
p_image_id,
v_product_id;

EXCEPTION
WHEN OTHERS THEN -- Handle exceptions (optional)
RAISE EXCEPTION 'Error setting primary image for image_id %: %',
p_image_id,
SQLERRM;

END;

$ $;

ALTER FUNCTION public.set_primary_image(integer) OWNER TO postgres;

-------------------------------------------------------------
-- FUNCTION: public.insert_product_with_images(text, text, integer, integer, integer, numeric, numeric, integer, jsonb)
-- DROP FUNCTION IF EXISTS public.insert_product_with_images(text, text, integer, integer, integer, numeric, numeric, integer, jsonb);
CREATE
OR REPLACE FUNCTION public.insert_product_with_images(
  p_name text,
  p_description text,
  p_brand_id integer,
  p_category_id integer,
  p_size_id integer,
  p_price numeric,
  p_stock numeric,
  p_color_id integer,
  p_images jsonb
) RETURNS integer LANGUAGE 'plpgsql' COST 100 VOLATILE PARALLEL UNSAFE AS $ BODY $ DECLARE v_product_id INTEGER;

BEGIN -- Start a transaction
BEGIN -- Insert the product and get its ID
INSERT INTO
  public.products (
    name,
    description,
    brand_id,
    category_id,
    size_id,
    color_id,
    price,
    stock
  )
VALUES
  (
    p_name,
    p_description,
    p_brand_id,
    p_category_id,
    p_size_id,
    p_color_id,
    p_price,
    p_stock
  ) RETURNING id INTO v_product_id;

-- Insert the images
INSERT INTO
  public.product_images (product_id, image_url, is_primary, alt_text)
SELECT
  v_product_id,
  image_data ->> 'ImageUrl',
  (image_data ->> 'IsPrimary') :: BOOLEAN,
  image_data ->> 'AltText'
FROM
  jsonb_array_elements(p_images) AS image_data;

RETURN v_product_id;

END;

END;

$ BODY $;

ALTER FUNCTION public.insert_product_with_images(
  text,
  text,
  integer,
  integer,
  integer,
  numeric,
  numeric,
  integer,
  jsonb
) OWNER TO postgres;

----------------------------------------------------
-- FUNCTION: public.insert_product_image(integer, text, text, boolean)
-- DROP FUNCTION IF EXISTS public.insert_product_image(integer, text, text, boolean);
CREATE
OR REPLACE FUNCTION public.insert_product_image(
  p_product_id integer,
  p_image_url text,
  p_alt_text text,
  p_is_primary boolean
) RETURNS integer LANGUAGE 'plpgsql' COST 100 VOLATILE PARALLEL UNSAFE AS $ BODY $ DECLARE v_image_id INTEGER;

BEGIN -- Step 1: If the image is marked as primary, set all other images for this product to non-primary
IF p_is_primary THEN
UPDATE
  public.product_images
SET
  is_primary = false
WHERE
  product_id = p_product_id;

END IF;

-- Step 2: Insert the new image
INSERT INTO
  public.product_images (
    product_id,
    image_url,
    alt_text,
    is_primary,
    create_date,
    update_date
  )
VALUES
  (
    p_product_id,
    p_image_url,
    p_alt_text,
    p_is_primary,
    CURRENT_TIMESTAMP,
    CURRENT_TIMESTAMP
  ) RETURNING id INTO v_image_id;

-- Step 3: Return the newly inserted image ID
RETURN v_image_id;

EXCEPTION
WHEN OTHERS THEN -- Raise an error in case of failure
RAISE EXCEPTION 'Error inserting product image: %',
SQLERRM;

RETURN NULL;

END;

$ BODY $;

ALTER FUNCTION public.insert_product_image(integer, text, text, boolean) OWNER TO postgres;

----------------------------------------------------
-- FUNCTION: public.insert_or_update_cart(integer, integer, integer)
-- DROP FUNCTION IF EXISTS public.insert_or_update_cart(integer, integer, integer);
CREATE
OR REPLACE FUNCTION public.insert_or_update_cart(
  p_user_id integer,
  p_product_id integer,
  p_quantity integer
) RETURNS integer LANGUAGE 'plpgsql' COST 100 VOLATILE PARALLEL UNSAFE AS $ BODY $ DECLARE existing_quantity INT;

stock_quantity INT;

v_cart_detail_id INT;

BEGIN -- Check the current stock for the product variant
SELECT
  stock INTO stock_quantity
FROM
  products
WHERE
  id = p_product_id;

IF stock_quantity IS NULL THEN RAISE EXCEPTION 'Product  % does not exist.',
p_product_id;

END IF;

-- Ensure enough stock is available
IF p_quantity > stock_quantity THEN RAISE EXCEPTION 'Not enough stock for product  %. Available: %, Requested: %',
p_product_id,
stock_quantity,
p_quantity;

END IF;

-- Check if the product already exists in the cart
SELECT
  quantity INTO existing_quantity
FROM
  cart_details
WHERE
  user_id = p_user_id
  AND product_id = p_product_id;

IF FOUND THEN IF p_quantity + existing_quantity > stock_quantity THEN RAISE EXCEPTION 'Not enough stock for product variant %. Available: %, Requested: %',
p_product_id,
stock_quantity,
p_quantity + existing_quantity;

END IF;

-- Update existing cart item
UPDATE
  cart_details
SET
  quantity = existing_quantity + p_quantity
WHERE
  user_id = p_user_id
  AND product_id = p_product_id RETURNING id INTO v_cart_detail_id;

ELSE -- Insert new cart item
INSERT INTO
  cart_details (user_id, product_id, quantity)
VALUES
  (p_user_id, p_product_id, p_quantity) RETURNING id INTO v_cart_detail_id;

END IF;

RETURN v_cart_detail_id;

END;

$ BODY $;

ALTER FUNCTION public.insert_or_update_cart(integer, integer, integer) OWNER TO postgres;

--------------------------------------------------
-- FUNCTION: public.cart_checkout(integer)
-- DROP FUNCTION IF EXISTS public.cart_checkout(integer);
CREATE
OR REPLACE FUNCTION public.cart_checkout(p_user_id integer) RETURNS integer LANGUAGE 'plpgsql' COST 100 VOLATILE PARALLEL UNSAFE AS $ BODY $ DECLARE new_order_id INT;

details RECORD;

BEGIN
INSERT INTO
  orders (user_id)
VALUES
  (p_user_id) RETURNING id INTO new_order_id;

IF NOT EXISTS (
  SELECT
    1
  FROM
    cart_details
  WHERE
    user_id = p_user_id
) THEN RAISE EXCEPTION 'No cart Details found for user with ID %',
p_user_id;

END IF;

FOR details IN
SELECT
  ci.product_id,
  ci.quantity
FROM
  cart_details ci
WHERE
  ci.user_id = p_user_id LOOP IF NOT EXISTS (
    SELECT
      1
    FROM
      products p
    WHERE
      p.id = details.product_id
      AND p.stock >= details.quantity
  ) THEN RAISE EXCEPTION 'Insufficient stock for product ID %',
  details.product_id;

END IF;

INSERT INTO
  order_details (price, discount, quantity, order_id, product_id)
SELECT
  ROUND(sum(p.price), 2),
  ROUND(
    sum(
      p.price * (COALESCE(d.discount_percentage, 0)) / 100.0
    ),
    2
  ),
  details.quantity,
  new_order_id,
  details.product_id
FROM
  products p
  LEFT JOIN discounts d ON d.product_id = p.id
  AND CURRENT_TIMESTAMP >= d.start_date
  AND CURRENT_TIMESTAMP <= d.end_date
WHERE
  p.id = details.product_id;

UPDATE
  products
SET
  stock = stock - details.quantity
WHERE
  id = details.product_id;

END LOOP;

DELETE FROM
  cart_details
WHERE
  user_id = p_user_id;

UPDATE
  Orders
SET
  total = (
    Select
      ROUND(
        sum(p.price * od.quantity),
        2
      )
    FROM
      products p
      JOIN order_details od ON od.product_id = p.id
      JOIN orders o ON o.id = od.order_id
      LEFT JOIN discounts d ON d.product_id = p.id
      AND o.order_date >= d.start_date
      AND o.order_date <= d.end_date
    WHERE
      od.order_id = new_order_id
  ),
  discount = (
    Select
      ROUND(
        sum(
          p.price * (COALESCE(d.discount_percentage, 0)) / 100.0 * od.quantity
        ),
        2
      )
    FROM
      products p
      JOIN order_details od ON od.product_id = p.id
      JOIN orders o ON o.id = od.order_id
      LEFT JOIN discounts d ON d.product_id = p.id
      AND o.order_date >= d.start_date
      AND o.order_date <= d.end_date
    WHERE
      od.order_id = new_order_id
  )
WHERE
  id = new_order_id;

return new_order_id;

EXCEPTION
WHEN OTHERS THEN RAISE;

END;

$ BODY $;

ALTER FUNCTION public.cart_checkout(integer) OWNER TO postgres;

---------------------------------------------------------------------------------

-- FUNCTION: public.update_product_image(integer, text, text, boolean)
-- DROP FUNCTION IF EXISTS public.update_product_image(integer, text, text, boolean);
CREATE
OR REPLACE FUNCTION public.update_product_image(
  p_image_id integer,
  p_image_url text,
  p_alt_text text,
  p_is_primary boolean
) RETURNS boolean LANGUAGE 'plpgsql' COST 100 VOLATILE PARALLEL UNSAFE AS $ BODY $ DECLARE v_product_id INTEGER;

BEGIN -- Step 1: Find the product_id based on the image_id
SELECT
  product_id INTO v_product_id
FROM
  public.product_images
WHERE
  id = p_image_id;

-- If the product_id is not found, raise an exception
IF v_product_id IS NULL THEN RAISE EXCEPTION 'No product found for image_id %',
p_image_id;

END IF;

-- Step 2: Update the image_url and alt_text for the given image_id
UPDATE
  public.product_images
SET
  image_url = p_image_url,
  alt_text = p_alt_text,
  update_date = CURRENT_TIMESTAMP
WHERE
  id = p_image_id;

-- Step 3: Set the image as primary if p_is_primary is true
IF p_is_primary THEN -- Step 3a: Set all other images for this product to not primary
UPDATE
  public.product_images
SET
  is_primary = false
WHERE
  product_id = v_product_id;

-- Step 3b: Set the given image as primary
UPDATE
  public.product_images
SET
  is_primary = true
WHERE
  id = p_image_id;

END IF;

-- Optional success message
return true;

EXCEPTION
WHEN OTHERS THEN -- Handle errors if something goes wrong
RAISE EXCEPTION 'Error updating image %: %',
p_image_id,
SQLERRM;

END;

$ BODY $;

ALTER FUNCTION public.update_product_image(integer, text, text, boolean) OWNER TO postgres;