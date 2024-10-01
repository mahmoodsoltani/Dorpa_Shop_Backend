using System.Linq.Expressions;
using System.Reflection.Metadata;
using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Infrastructure.src.Repository.Shared;
using Ecommerce.Infrastructure.src.Service;
using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository;
using Ecommerce.Model.src.Repository.UserRepoAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.AuthService;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository
{
    public class UserRepo(AppDbContext appDbContext, IPasswordHasher passwordHasher)
        : BaseRepo<User>(appDbContext),
            IUserRepo
    {
        private IPasswordHasher _passwordHasher = passwordHasher;

        public override async Task<User> CreateAsync(
            User entity,
            Expression<Func<User, bool>>? checkDuplicate = null,
            Expression<Func<User, object>>[] includes = null
        )
        {
            if (checkDuplicate != null)
            {
                IQueryable<User> query = _dbSet;
                var result = await query.AnyAsync(checkDuplicate);
                if (result)
                {
                    throw new RegistrationException("An account already exists with this email.");
                }
            }
            ArgumentNullException.ThrowIfNull(entity);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            var addedEntity = await GetAsync(e => e.Id == entity.Id, includes, false);
            return addedEntity;
        }

        public async Task<bool> UpdatePasswordAsync(int userId, string newPassword)
        {
            var user =
                await _dbSet.FindAsync(userId) ?? throw new EntityNotFoundException("User", userId);

            _passwordHasher.HashPassword(newPassword, out string hashedPassword, out byte[] salt);
            user.Salt = salt;
            user.Password = hashedPassword;

            _dbSet.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
