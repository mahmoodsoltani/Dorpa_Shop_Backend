using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Infrastructure.src.Repository.Shared;
using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.UserRepoAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository.UserRepoAggregate
{
    public class FavouriteRepo(AppDbContext appDbContext)
        : BaseRepo<Favourite>(appDbContext),
            IFavouriteRepo
    {
        AppDbContext _context = appDbContext;

        public async Task<bool> DeleteByData(int userId, int productId)
        {
            var entity =
                _dbSet.FirstOrDefault(e => e.UserId == userId && e.ProductId == productId)
                ?? throw new EntityNotFoundException();
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
