using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.UserServiceAggregate.UserAggregate
{
    public interface IUserService
        : IBaseService<
            User,
            UserReadDto,
            UserUpdateDto,
            UserCreateDto,
            UserUpdateValidator,
            UserCreateValidator
        > { }
}
