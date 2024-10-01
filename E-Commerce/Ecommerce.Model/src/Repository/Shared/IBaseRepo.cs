using System.Linq.Expressions;
using Ecommerce.Model.src.Shared.ValueObject;

namespace Ecommerce.Model.src.Repository
{
    public interface IBaseRepo<T>
        where T : BaseEntity
    {
        Task<PaginatedResult<T>> GetAllAsync(
            QueryOptions paginationOptions,
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>[] includes = null,
            bool tracked = true
        );
        Task<T> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>[] includes = null,
            bool tracked = true
        );
        Task<T> CreateAsync(
            T entity,
            Expression<Func<T, bool>>? checkDuplicate = null,
            Expression<Func<T, object>>[] includes = null
        );
        Task<T> UpdateAsync(T entity, Expression<Func<T, bool>>? checkDuplicate = null);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>>? filter = null);
    }
}
