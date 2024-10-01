using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Repository.UserRepoAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;

namespace Ecommerce.Service.src.UserServiceAggregate.FavoutiteAggregate
{
    public class FavouriteService(IFavouriteRepo favouriteRepo)
        : BaseService<
            Favourite,
            FavouriteReadDto,
            FavouriteUpdateDto,
            FavouriteCreateDto,
            FavouriteUpdateValidator,
            FavouriteCreateValidator
        >(favouriteRepo),
            IFavouriteService
    {
        IFavouriteRepo _favouriteRepo = favouriteRepo;

        public async Task<bool> DeleteByData(int userId, int productId)
        {
            return await _favouriteRepo.DeleteByData(userId, productId);
        }
    }
}
