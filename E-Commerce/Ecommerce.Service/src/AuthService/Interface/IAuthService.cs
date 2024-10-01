using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.UserServiceAggregate.UserAggregate;

namespace Ecommerce.Service.src.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(UserCredentials userCredentials);
        Task LogoutAsync(string username);
        Task<LoginResponse> LoginWithGoogleAsync(UserCreateDto createDto);
    }
}
