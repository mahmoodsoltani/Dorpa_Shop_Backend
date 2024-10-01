using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    alt_text = table.Column<string>(type: "text", nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brands", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    parent_id = table.Column<int>(type: "integer", nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_categories_categories_parent_id",
                        column: x => x.parent_id,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "colors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_colors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sizes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sizes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    is_admin = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "FALSE"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "FALSE"),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    brand_id = table.Column<int>(type: "integer", nullable: true),
                    category_id = table.Column<int>(type: "integer", nullable: true),
                    stock = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "0"),
                    size_id = table.Column<int>(type: "integer", nullable: true),
                    color_id = table.Column<int>(type: "integer", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.CheckConstraint("CK_Product_Price_AboveZero", "price> 0");
                    table.ForeignKey(
                        name: "fk_products_brands_brand_id",
                        column: x => x.brand_id,
                        principalTable: "brands",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_products_colors_color_id",
                        column: x => x.color_id,
                        principalTable: "colors",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_products_sizes_size_id",
                        column: x => x.size_id,
                        principalTable: "sizes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    street = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    state = table.Column<string>(type: "text", nullable: true),
                    postal_code = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_addresses_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    total = table.Column<decimal>(type: "numeric", nullable: true),
                    discount = table.Column<decimal>(type: "numeric", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_cart_details_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cart_details_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "discounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    discount_percentage = table.Column<decimal>(type: "numeric", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discounts", x => x.id);
                    table.CheckConstraint("CK_Discount_Date", "start_Date < end_Date");
                    table.ForeignKey(
                        name: "fk_discounts_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "favourites",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_favourites", x => x.id);
                    table.ForeignKey(
                        name: "fk_favourites_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_favourites_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_images",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    alt_text = table.Column<string>(type: "text", nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_images_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    message = table.Column<string>(type: "text", nullable: false),
                    review_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rate = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    discount = table.Column<decimal>(type: "numeric", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_details", x => x.id);
                    table.CheckConstraint("CK_OrderDetail_Price_AboveZero", "price > 0");
                    table.CheckConstraint("CK_OrderDetail_Quantity_Positive", "quantity > 0");
                    table.ForeignKey(
                        name: "fk_order_details_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_details_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "brands",
                columns: new[] { "id", "alt_text", "create_date", "description", "image_url", "name", "update_date" },
                values: new object[,]
                {
                    { 1, "Trek logo", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7010), "Trek is a leading bicycle manufacturer known for its high-performance bikes and innovative designs.", "https://example.com/trek.jpg", "Trek", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7011) },
                    { 2, "Specialized logo", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7019), "Specialized offers a wide range of bikes for different riding styles and disciplines.", "https://example.com/specialized.jpg", "Specialized", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7020) },
                    { 3, "Giant logo", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7022), "Giant is one of the largest bike manufacturers globally, known for producing reliable and affordable bicycles.", "https://example.com/giant.jpg", "Giant", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7023) },
                    { 4, "Cannondale logo", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7025), "Cannondale is renowned for its innovation in bicycle technology and design.", "https://example.com/cannondale.jpg", "Cannondale", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7025) },
                    { 5, "Bianchi logo", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7027), "Bianchi is an Italian brand with a long history of producing high-quality road bikes and racing bicycles.", "https://example.com/bianchi.jpg", "Bianchi", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7028) },
                    { 6, "Scott logo", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7032), "Scott Sports is known for its range of high-performance bicycles and cycling gear.", "https://example.com/scott.jpg", "Scott", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7032) },
                    { 7, "Santa Cruz logo", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7034), "Santa Cruz specializes in high-end mountain bikes with a focus on innovation and performance.", "https://example.com/santacruz.jpg", "Santa Cruz", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7035) },
                    { 8, "Norco logo", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7037), "Norco is a Canadian brand known for its mountain bikes and cycling products.", "https://example.com/norco.jpg", "Norco", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7038) },
                    { 9, "Cube logo", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7040), "Cube offers a wide range of bicycles, including road, mountain, and e-bikes.", "https://example.com/cube.jpg", "Cube", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7040) },
                    { 10, "Raleigh logo", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7043), "Raleigh is a historic brand offering a diverse range of bicycles.", "https://example.com/raleigh.jpg", "Raleigh", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7044) }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "create_date", "description", "name", "parent_id", "update_date" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6767), "All types of bicycles", "Bicycles", null, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6767) },
                    { 28, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6846), "Bike accessories", "Accessories", null, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6846) },
                    { 33, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6859), "Bike parts", "Parts", null, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6860) },
                    { 39, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6926), "Cycling clothing", "Clothing", null, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6927) }
                });

            migrationBuilder.InsertData(
                table: "colors",
                columns: new[] { "id", "code", "create_date", "name", "update_date" },
                values: new object[,]
                {
                    { 1, "#FF0000", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6439), "Red", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6439) },
                    { 2, "#00FF00", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6444), "Green", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6445) },
                    { 3, "#0000FF", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6447), "Blue", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6447) },
                    { 4, "#FFFF00", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6450), "Yellow", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6450) },
                    { 5, "#00FFFF", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6452), "Cyan", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6453) },
                    { 6, "#FF00FF", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6459), "Magenta", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6459) },
                    { 7, "#000000", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6538), "Black", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6539) },
                    { 8, "#FFFFFF", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6541), "White", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6542) },
                    { 9, "#808080", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6544), "Gray", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6544) },
                    { 10, "#800000", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6547), "Maroon", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6547) },
                    { 11, "#808000", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6549), "Olive", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6550) },
                    { 12, "#800080", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6552), "Purple", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6552) },
                    { 13, "#C0C0C0", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6554), "Silver", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6555) },
                    { 14, "#008080", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6557), "Teal", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6557) },
                    { 15, "#000080", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6559), "Navy", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6560) },
                    { 16, "#00FF00", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6562), "Lime", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6562) },
                    { 17, "#00FFFF", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6564), "Aqua", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6564) },
                    { 18, "#FF00FF", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6568), "Fuchsia", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6569) },
                    { 19, "#FFA500", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6571), "Orange", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6571) },
                    { 20, "#A52A2A", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6573), "Brown", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6574) },
                    { 21, "#FFC0CB", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6576), "Pink", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6576) },
                    { 22, "#4B0082", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6578), "Indigo", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6579) }
                });

            migrationBuilder.InsertData(
                table: "sizes",
                columns: new[] { "id", "create_date", "name", "update_date" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6660), "Small", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6661) },
                    { 2, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6665), "Medium", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6665) },
                    { 3, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6667), "Large", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6668) },
                    { 4, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6670), "Extra Large", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6670) },
                    { 5, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6672), "XXL", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6673) },
                    { 6, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6676), "XS", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6676) },
                    { 7, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6678), "XXS", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6679) },
                    { 8, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6681), "One Size", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6681) },
                    { 9, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6683), "Youth", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6683) },
                    { 10, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6686), "Adult", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6687) },
                    { 11, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6689), "Regular", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6689) },
                    { 12, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6691), "Custom", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6691) },
                    { 13, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6693), "Standard", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6694) },
                    { 14, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6695), "Slim", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6696) },
                    { 15, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6698), "Fit", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6698) },
                    { 16, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6700), "Classic", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6701) },
                    { 17, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6703), "Plus Size", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6703) },
                    { 18, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6706), "Petite", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6706) },
                    { 19, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6708), "Tall", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6709) },
                    { 20, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6710), "Short", new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6711) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "create_date", "email", "first_name", "is_admin", "last_name", "password", "phone_number", "salt", "update_date" },
                values: new object[] { 1, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6297), "mahmoud.soltani@gmail.com", "Mahmoud", true, "Soltani", "25SvyWDSh0Nw1YYWLQWGq3KoLw0kiIFrP5PJYe1i2Uw=", null, new byte[] { 53, 38, 7, 94, 193, 42, 228, 43, 105, 161, 180, 46, 43, 193, 42, 58 }, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6300) });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "create_date", "email", "first_name", "last_name", "password", "phone_number", "salt", "update_date" },
                values: new object[,]
                {
                    { 2, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6317), "John@gmail.com", "John", "Doe", "25SvyWDSh0Nw1YYWLQWGq3KoLw0kiIFrP5PJYe1i2Uw=", null, new byte[] { 53, 38, 7, 94, 193, 42, 228, 43, 105, 161, 180, 46, 43, 193, 42, 58 }, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6318) },
                    { 3, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6320), "mike@gmail.com", "Mike", "Smite", "25SvyWDSh0Nw1YYWLQWGq3KoLw0kiIFrP5PJYe1i2Uw=", null, new byte[] { 53, 38, 7, 94, 193, 42, 228, 43, 105, 161, 180, 46, 43, 193, 42, 58 }, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6320) }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "create_date", "description", "name", "parent_id", "update_date" },
                values: new object[,]
                {
                    { 2, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6772), "Off-road bikes", "Mountain Bikes", 1, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6772) },
                    { 9, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6792), "Lightweight bikes for paved roads", "Road Bikes", 1, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6793) },
                    { 14, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6807), "Versatile bikes for various terrains", "Hybrid Bikes", 1, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6807) },
                    { 17, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6815), "Bikes with electric assistance", "Electric Bikes", 1, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6816) },
                    { 21, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6828), "Bicycles designed for children", "Kids' Bikes", 1, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6828) },
                    { 25, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6838), "Bicycles and accessories for men", "Men", 1, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6839) },
                    { 26, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6841), "Bicycles and accessories for women", "Women", 1, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6841) },
                    { 27, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6843), "Bicycles and accessories for children", "Children", 1, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6844) },
                    { 29, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6849), "Safety helmets", "Helmets", 28, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6849) },
                    { 30, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6851), "Bike lights", "Lights", 28, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6852) },
                    { 31, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6854), "Bike locks", "Locks", 28, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6854) },
                    { 32, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6857), "Bike bags and carriers", "Bags", 28, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6857) },
                    { 34, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6863), "Drivetrain components", "Drivetrain", 33, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6863) },
                    { 35, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6865), "Brakes and braking components", "Brakes", 33, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6866) },
                    { 36, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6918), "Wheels and rims", "Wheels", 33, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6919) },
                    { 37, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6921), "Bike tires", "Tires", 33, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6922) },
                    { 38, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6924), "Handlebars and grips", "Handlebars", 33, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6924) },
                    { 40, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6929), "Cycling jerseys", "Jerseys", 39, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6929) },
                    { 41, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6932), "Cycling shorts", "Shorts", 39, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6932) },
                    { 42, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6934), "Cycling gloves", "Gloves", 39, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6935) },
                    { 43, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6937), "Cycling jackets", "Jackets", 39, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6938) },
                    { 44, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6940), "Cycling socks", "Socks", 39, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6940) }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "brand_id", "category_id", "color_id", "create_date", "description", "name", "price", "size_id", "stock", "update_date" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7091), "High-performance mountain bike with advanced suspension.", "Mountain Bike X1", 1200.00m, 1, 10, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7091) },
                    { 10, 10, 1, 10, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7131), "Durable mountain bike with enhanced shock absorbers.", "Mountain Bike Y2", 1300.00m, 1, 7, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7132) },
                    { 17, 1, 1, 7, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7158), "Advanced mountain bike with reinforced frame.", "Mountain Bike M2", 1400.00m, 1, 4, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7159) }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "create_date", "description", "name", "parent_id", "update_date" },
                values: new object[,]
                {
                    { 3, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6775), "XC mountain bikes", "Cross-Country (XC)", 2, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6775) },
                    { 6, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6784), "Trail bikes for rugged terrain", "Trail Bikes", 2, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6785) },
                    { 7, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6787), "Downhill mountain bikes", "Downhill Bikes", 2, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6787) },
                    { 8, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6790), "Enduro mountain bikes", "Enduro Bikes", 2, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6790) },
                    { 10, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6796), "Bikes for racing", "Race Bikes", 9, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6797) },
                    { 12, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6801), "Comfort-oriented road bikes", "Endurance Bikes", 9, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6802) },
                    { 13, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6804), "Aerodynamic bikes for speed", "Aero Bikes", 9, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6805) },
                    { 15, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6809), "Hybrid bikes suited for urban environments", "Urban Hybrid Bikes", 14, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6810) },
                    { 16, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6812), "Hybrid bikes designed for fitness", "Fitness Bikes", 14, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6813) },
                    { 18, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6819), "Electric bikes for mountain terrain", "E-Mountain Bikes", 17, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6819) },
                    { 19, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6822), "Electric road bikes", "E-Road Bikes", 17, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6822) },
                    { 20, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6825), "Electric hybrid bikes", "E-Hybrid Bikes", 17, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6825) },
                    { 22, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6830), "Bikes for toddlers to learn balance", "Balance Bikes", 21, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6831) },
                    { 23, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6833), "Traditional pedal bikes for kids", "Pedal Bikes", 21, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6833) },
                    { 24, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6836), "Mountain bikes designed for kids", "Mountain Kids Bikes", 21, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6836) }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "brand_id", "category_id", "color_id", "create_date", "description", "name", "price", "size_id", "stock", "update_date" },
                values: new object[,]
                {
                    { 2, 2, 2, 2, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7098), "Lightweight road bike designed for speed and efficiency.", "Road Bike Pro", 1500.00m, 2, 5, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7099) },
                    { 9, 9, 2, 9, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7126), "High-speed racing bike with aerodynamic design.", "Racing Bike S1", 1800.00m, 2, 4, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7127) },
                    { 16, 10, 2, 6, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7154), "High-performance bike designed for speed enthusiasts.", "Performance Bike P1", 1500.00m, 2, 8, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7155) },
                    { 19, 3, 2, 9, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7168), "Speed-oriented road bike with aerodynamic frame.", "Road Bike R2", 1700.00m, 2, 6, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7168) }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "create_date", "description", "name", "parent_id", "update_date" },
                values: new object[,]
                {
                    { 4, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6778), "Hardtail cross-country mountain bikes", "Hardtail XC", 3, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6778) },
                    { 5, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6780), "Full-suspension cross-country mountain bikes", "Full-Suspension XC", 3, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6781) },
                    { 11, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6799), "Lightweight carbon frame race bikes", "Carbon Frame Race Bikes", 10, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(6799) }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "brand_id", "category_id", "color_id", "create_date", "description", "name", "price", "size_id", "stock", "update_date" },
                values: new object[,]
                {
                    { 3, 3, 3, 3, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7102), "Versatile hybrid bike for both urban and off-road adventures.", "Hybrid Bike ZX", 900.00m, 3, 7, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7102) },
                    { 6, 6, 6, 6, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7115), "Colorful and durable bike designed for children.", "Kids Bike Fun", 300.00m, 4, 12, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7115) },
                    { 7, 7, 7, 7, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7118), "Comfortable bike for city commuting and leisure rides.", "Comfort Bike C1", 800.00m, 3, 8, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7119) },
                    { 8, 8, 8, 8, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7122), "Compact foldable bike ideal for urban environments.", "Foldable Bike X2", 700.00m, 3, 9, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7123) },
                    { 12, 6, 7, 2, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7139), "Stylish cruiser bike with comfortable seating.", "Cruiser Bike C2", 950.00m, 2, 6, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7139) },
                    { 13, 7, 6, 3, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7143), "Durable bike for kids with adjustable seat.", "Kids Bike X3", 350.00m, 4, 20, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7143) },
                    { 14, 8, 3, 4, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7147), "Hybrid bike suitable for both road and trail riding.", "Hybrid Bike Z3", 1000.00m, 1, 9, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7147) },
                    { 20, 4, 7, 10, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7172), "Classic cruiser bike with retro design.", "Cruiser Bike C3", 950.00m, 3, 7, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7172) },
                    { 4, 4, 4, 4, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7106), "Electric bike with long battery life and powerful motor.", "Electric Bike E1", 2000.00m, 1, 3, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7106) },
                    { 5, 5, 5, 5, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7110), "Comfortable touring bike with ample storage and stability.", "Touring Bike Expert", 1100.00m, 2, 6, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7110) },
                    { 11, 4, 4, 1, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7135), "Electric scooter with compact design and fast charging.", "Electric Scooter E2", 600.00m, 3, 15, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7136) },
                    { 15, 9, 5, 5, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7150), "High-comfort touring bike with multiple gears.", "Touring Bike T1", 1200.00m, 2, 5, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7151) },
                    { 18, 2, 4, 8, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7164), "Powerful electric bike with extended range.", "Electric Bike E3", 2200.00m, 3, 3, new DateTime(2024, 9, 5, 19, 32, 43, 219, DateTimeKind.Utc).AddTicks(7164) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_addresses_user_id",
                table: "addresses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_cart_details_product_id",
                table: "cart_details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_cart_details_user_id",
                table: "cart_details",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_categories_parent_id",
                table: "categories",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_discounts_product_id",
                table: "discounts",
                column: "product_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_favourites_product_id",
                table: "favourites",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_favourites_user_id",
                table: "favourites",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_details_order_id",
                table: "order_details",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_details_product_id",
                table: "order_details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_user_id",
                table: "orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_images_product_id",
                table: "product_images",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_brand_id",
                table: "products",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_color_id",
                table: "products",
                column: "color_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_size_id",
                table: "products",
                column: "size_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_product_id",
                table: "reviews",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_user_id",
                table: "reviews",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "cart_details");

            migrationBuilder.DropTable(
                name: "discounts");

            migrationBuilder.DropTable(
                name: "favourites");

            migrationBuilder.DropTable(
                name: "order_details");

            migrationBuilder.DropTable(
                name: "product_images");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "brands");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "colors");

            migrationBuilder.DropTable(
                name: "sizes");
        }
    }
}
