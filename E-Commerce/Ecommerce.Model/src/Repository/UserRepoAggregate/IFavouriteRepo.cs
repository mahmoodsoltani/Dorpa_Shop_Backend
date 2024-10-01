using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Shared.ValueObject;

namespace Ecommerce.Model.src.Repository.UserRepoAggregate
{
    public interface IFavouriteRepo : IBaseRepo<Favourite>
    {
        Task<bool> DeleteByData(int userId, int productId);
    }
}
