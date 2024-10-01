using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Shared.ValueObject;

namespace Ecommerce.Model.src.Repository.ProductRepoAggreate
{
    public interface IProductRepo : IBaseRepo<Product>
    {
        // Get list of the most purchased products
        Task<PaginatedResult<Product>> GetMostPurchasedProductsAsync(QueryOptions queryOptions);
    }
}
