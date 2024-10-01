using Ecommerce.Model.src.Shared.ValueObject;

namespace Ecommerce.Service.src.AuthService
{
    public interface ITokenService
    {
        string CreateToken(TokenOptions tokenOptions); // asymmetric hashing, no sensitive info
    }
}
