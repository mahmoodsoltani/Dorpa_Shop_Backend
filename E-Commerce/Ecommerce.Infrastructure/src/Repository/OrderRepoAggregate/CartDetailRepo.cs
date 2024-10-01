using System.Linq.Expressions;
using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Infrastructure.src.Repository.Shared;
using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.OrderRepoAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.Dto;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Ecommerce.Infrastructure.src.Repository.OrderRepoAggregate
{
    public class CartDetailRepo(AppDbContext context)
        : BaseRepo<CartDetail>(context),
            ICartDetailRepo
    {
        AppDbContext _context = context;

        public override async Task<CartDetail> CreateAsync(
            CartDetail entity,
            Expression<Func<CartDetail, bool>>? checkDuplicate = null,
            Expression<Func<CartDetail, object>>[] includes = null
        )
        {
            try
            {

                string query = "SELECT insert_or_update_cart({0},{1},{2})";
                object[] parameters = [entity.UserId, entity.ProductId, entity.Quantity];
                var cartDetailId =
                    await _context.Database.SqlQueryRaw<int>(query, parameters).ToListAsync()
                    ?? throw new OperationFailedException("Create cart detail", "Database Error");

                var cartDetail =
                    await _dbSet
                    .Include(cd => cd.Product).ThenInclude(p => p.Discount)
                    .SingleOrDefaultAsync(e => e.Id == cartDetailId.FirstOrDefault())
                    ?? throw new EntityNotFoundException("Cart detail", cartDetailId);

                return cartDetail;
            }
            catch(Exception e)
            {
                throw new OperationFailedException("Create cart detail",e.Message);
            }
        }
    }
}
