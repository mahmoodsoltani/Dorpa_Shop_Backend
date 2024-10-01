using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Infrastructure.src.Repository.Shared;
using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.ProductRepoAggreate;
using Ecommerce.Model.src.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace Ecommerce.Infrastructure.src.Repository.ProductRepoAggreate
{
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {
        public ProductRepo(AppDbContext context)
            : base(context) { }

        public Task<PaginatedResult<Product>> GetMostPurchasedProductsAsync(
            QueryOptions queryOptions
        )
        {
            throw new NotImplementedException();
        }

        public override async Task<Product> CreateAsync(
            Product product,
            Expression<Func<Product, bool>>? checkDuplicate = null,
            Expression<Func<Product, object>>[] includes = null
        )
        {
            var imageJson = JsonSerializer.Serialize(product.ProductImages);
            var query =
                "SELECT insert_product_with_images(@p_name, @p_description, @p_brand_id, @p_category_id, @p_size_id, @p_price,@p_stock,@p_color_id, @p_images)";
            var parameters = new[]
            {
                new NpgsqlParameter("@p_name", product.Name),
                new NpgsqlParameter("@p_description", product.Description ?? ""),
                new NpgsqlParameter("@p_brand_id", NpgsqlDbType.Integer)
                {
                    Value = product.BrandId == null ? DBNull.Value : product.BrandId
                },
                new NpgsqlParameter("@p_category_id", NpgsqlDbType.Integer)
                {
                    Value = product.CategoryId == null ? DBNull.Value : product.CategoryId
                },
                new NpgsqlParameter("@p_size_id", product.SizeId),
                new NpgsqlParameter("@p_price", product.Price),
                new NpgsqlParameter("@p_color_id", product.ColorId),
                new NpgsqlParameter("@p_stock", product.Stock),
                new NpgsqlParameter("@p_images", NpgsqlTypes.NpgsqlDbType.Jsonb)
                {
                    Value = imageJson
                }
            };

            var newProductId =
                await _context.Database.SqlQueryRaw<int>(query, parameters).ToListAsync()
                ?? throw new OperationFailedException("Create Product", "Database Error");

            var newProduct =
                await _dbSet
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.ProductImages)
                    .Include(p => p.Color)
                    .Include(p => p.Size)
                    .FirstOrDefaultAsync(p => p.Id == newProductId.FirstOrDefault())
                ?? throw new EntityNotFoundException("Product", newProductId);
            return newProduct;
        }
    }
}
