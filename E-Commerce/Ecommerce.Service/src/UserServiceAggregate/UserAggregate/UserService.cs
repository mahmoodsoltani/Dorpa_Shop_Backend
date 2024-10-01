using System.Linq.Expressions;
using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.UserRepoAggregate;
using Ecommerce.Service.src.AuthService;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.UserServiceAggregate.UserAggregate;

namespace Ecommerce.Service.src.UserServiceAggregate.UserAggregate
{
    public class UserService(IPasswordHasher passwordHasher, IUserRepo userRepo)
        : BaseService<
            User,
            UserReadDto,
            UserUpdateDto,
            UserCreateDto,
            UserUpdateValidator,
            UserCreateValidator
        >(userRepo),
            IUserService
    {
        private readonly IUserRepo _repo = userRepo;
        private IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<bool> UpdatePasswordAsync(int userId, string newPassword)
        {
            return await _repo.UpdatePasswordAsync(userId, newPassword);
        }

        public override async Task<UserReadDto> CreateAsync(
            UserCreateDto userCreateDto,
            Expression<Func<User, bool>>? checkDuplicate = null
        )
        {
            UserCreateValidator dataValidator = new();
            var result = dataValidator.Validate(userCreateDto);
            if (!result.IsValid)
            {
                throw new InvalidInputDataException(result.ErrorMessage);
            }
            
            _passwordHasher.HashPassword(
                userCreateDto.Password,
                out string hashedPassword,
                out byte[] salt
            );
            userCreateDto.Password = hashedPassword;
            userCreateDto.Salt = salt;

            User entity = new();
            userCreateDto.ToEntity(entity);
            await _repo.CreateAsync(entity, checkDuplicate);
            UserReadDto readDto = new();
            readDto.FromEntity(entity);
            return readDto;
        }
    }
}
