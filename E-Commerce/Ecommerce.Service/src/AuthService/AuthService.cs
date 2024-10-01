using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.UserRepoAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.UserServiceAggregate.UserAggregate;

namespace Ecommerce.Service.src.AuthService
{
    public class AuthService(
        IUserRepo userRepo,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IUserService userService
    ) : IAuthService
    {
        private IUserRepo _userRepo = userRepo;
        private IUserService _userService = userService;
        private IPasswordHasher _passwordHasher = passwordHasher;
        private ITokenService _tokenService = tokenService;

        public async Task<LoginResponse> LoginAsync(UserCredentials userCredentials)
        {
            var foundUserByEmail =
                await _userRepo.GetAsync(
                    u => u.Email.ToLower() == userCredentials.Email.ToLower(),
                    null,
                    false
                ) ?? throw new AuthenticationException("Invalid Email or Password.");
            var isVerified = _passwordHasher.VerifyPassword(
                userCredentials.Password,
                foundUserByEmail.Password,
                foundUserByEmail.Salt
            );

            if (isVerified)
                return new LoginResponse
                {
                    Token = _tokenService.CreateToken(
                        new TokenOptions
                        {
                            Id = foundUserByEmail.Id,
                            Is_Admin = foundUserByEmail.IsAdmin,
                            Email = foundUserByEmail.Email
                        }
                    ),
                    Email = foundUserByEmail.Email,
                    FirstName = foundUserByEmail.FirstName,
                    LastName = foundUserByEmail.LastName,
                    Is_Admin = foundUserByEmail.IsAdmin,
                    Id = foundUserByEmail.Id
                };
            throw new AuthenticationException("Invalid Email or Password.");
        }

        public async Task<LoginResponse> LoginWithGoogleAsync(UserCreateDto createDto)
        {
            var existingUser = await _userRepo.GetAsync(
                u => u.Email.ToLower() == createDto.Email.ToLower(),
                null,
                false
            );

            if (existingUser != null)
            {
                return new LoginResponse
                {
                    Token = _tokenService.CreateToken(
                        new TokenOptions
                        {
                            Id = existingUser.Id,
                            Is_Admin = existingUser.IsAdmin,
                            Email = existingUser.Email
                        }
                    ),
                    Email = existingUser.Email,
                    FirstName = existingUser.FirstName,
                    LastName = existingUser.LastName,
                    Is_Admin = existingUser.IsAdmin,
                    Id = existingUser.Id
                };
            }

            // User does not exist, create a new user
         
            await _userService.CreateAsync(createDto);
            existingUser = await _userRepo.GetAsync(
                u => u.Email.ToLower() == createDto.Email.ToLower(),
                null,
                false
            );
            if (existingUser != null)
            {
                return new LoginResponse
                {
                    Token = _tokenService.CreateToken(
                        new TokenOptions
                        {
                            Id = existingUser.Id,
                            Is_Admin = existingUser.IsAdmin,
                            Email = existingUser.Email
                        }
                    ),
                    Email = existingUser.Email,
                    FirstName = existingUser.FirstName,
                    LastName = existingUser.LastName,
                    Is_Admin = existingUser.IsAdmin,
                    Id = existingUser.Id
                };
            }
            throw new AuthenticationException("Google Authentication failed! Please try later");
        }

        public async Task LogoutAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
