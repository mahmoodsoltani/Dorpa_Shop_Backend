using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.AuthService;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Infrastructure.src.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(TokenOptions tokenOptions)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, tokenOptions.Id.ToString()),
                new(ClaimTypes.Role, tokenOptions.Is_Admin ? "Admin" : "Customer")
            };
            var signingKey = _config["Jwt:Key"];
            var symmetricKey = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                SecurityAlgorithms.HmacSha256
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _config["Jwt:Issuer"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = symmetricKey
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
