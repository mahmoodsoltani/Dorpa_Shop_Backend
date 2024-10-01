using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.UserServiceAggregate.FavoutiteAggregate
{
    public interface IFavouriteService
        : IBaseService<
            Favourite,
            FavouriteReadDto,
            FavouriteUpdateDto,
            FavouriteCreateDto,
            FavouriteUpdateValidator,
            FavouriteCreateValidator
        >
    {
        Task<bool> DeleteByData(int userId, int productId);
    }
}
