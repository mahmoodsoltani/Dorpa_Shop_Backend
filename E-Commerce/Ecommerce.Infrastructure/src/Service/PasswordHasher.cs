using System.Security.Cryptography;
using Ecommerce.Service.src.AuthService;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Ecommerce.Infrastructure.src.Service
{
    public class PasswordHasher : IPasswordHasher
    {
        public void HashPassword(string inputPassword, out string hashedPassword, out byte[] salt)
        {
            salt = GenerateSalt(16);
            hashedPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: inputPassword,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 1000,
                    numBytesRequested: 32
                )
            );
        }

        public bool VerifyPassword(string inputPassword, string storedHashedPassword, byte[] salt)
        {
            var hashedInputPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: inputPassword,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 1000,
                    numBytesRequested: 32
                )
            );

            return hashedInputPassword == storedHashedPassword;
        }

        static byte[] GenerateSalt(int size)
        {
            byte[] salt = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
