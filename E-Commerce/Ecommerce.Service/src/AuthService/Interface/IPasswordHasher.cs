namespace Ecommerce.Service.src.AuthService
{
    public interface IPasswordHasher
    {
        void HashPassword(string inputPassword, out string hashedPassword, out byte[] salt);
        bool VerifyPassword(string inputPassword, string storedPassword, byte[] salt);
    }
}