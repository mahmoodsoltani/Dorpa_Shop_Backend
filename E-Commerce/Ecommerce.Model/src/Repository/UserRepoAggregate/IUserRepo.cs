using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Repository;
using Ecommerce.Model.src.Shared.ValueObject;

namespace Ecommerce.Model.src.Repository.UserRepoAggregate
{
    public interface IUserRepo : IBaseRepo<User>
    {
        Task<bool> UpdatePasswordAsync(int userId, string newPassword);
    }
}
