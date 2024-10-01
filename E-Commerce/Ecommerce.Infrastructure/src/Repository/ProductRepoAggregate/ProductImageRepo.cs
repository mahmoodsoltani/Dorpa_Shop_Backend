using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Infrastructure.src.Repository.Shared;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.ProductRepoAggreate;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository.ProductRepoAggreate
{
    public class ProductImageRepo : BaseRepo<ProductImage>, IProductImageRepo
    {
        public ProductImageRepo(AppDbContext context)
            : base(context) { }

        public override async Task<ProductImage> UpdateAsync(
            ProductImage entity,
            Expression<Func<ProductImage, bool>>? checkDuplicate = null
        )
        {
            try
            {
                string query = "SELECT update_product_image({0}, {1}, {2}, {3})";

                object[] parameters =
                {
                    entity.Id,
                    entity.ImageUrl,
                    entity.AltText ?? "",
                    entity.IsPrimary
                };

                var result =
                    await _context.Database.SqlQueryRaw<bool>(query, parameters).ToListAsync()
                    ?? throw new OperationFailedException("Update Product Image", "Database Error");
                if (result.FirstOrDefault())
                {
                    return entity;
                }
                else
                {
                    throw new OperationFailedException("Update Product Image", "Database Error");
                }
            }
            catch (Exception ex)
            {
                throw new OperationFailedException("Update product image", ex.Message);
            }
        }

        public override async Task<ProductImage> CreateAsync(
            ProductImage entity,
            Expression<Func<ProductImage, bool>>? checkDuplicate = null,
            Expression<Func<ProductImage, object>>[] includes = null
        )
        {
            try
            {
                string query = "SELECT public.insert_product_image({0}, {1}, {2}, {3})";

                object[] parameters =
                {
                    entity.ProductId,
                    entity.ImageUrl,
                    entity.AltText ?? "",
                    entity.IsPrimary
                };

                var imageId =
                    await _context.Database.SqlQueryRaw<int>(query, parameters).ToListAsync()
                    ?? throw new OperationFailedException("Create product Image", "Database Error");

                var productImage =
                    await _dbSet.SingleOrDefaultAsync(e => e.Id == imageId.FirstOrDefault())
                    ?? throw new EntityNotFoundException("ProductImage", imageId);

                return productImage;
            }
            catch (Exception ex)
            {
                // Log the exception and rethrow a custom exception if the operation fails
                throw new OperationFailedException("Insert product image", ex.Message);
            }
        }
    }
}
