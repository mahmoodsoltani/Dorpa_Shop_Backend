using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Infrastructure.src.Repository.Shared;
using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.OrderRepoAggregate;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository.OrderRepoAggregate
{
    public class OrderRepo(AppDbContext context) : BaseRepo<Order>(context), IOrderRepo
    {
        AppDbContext _context = context;

        public async Task<Order> CartCheckout(int userId)
        {
            try
            {
                string query = "SELECT cart_checkout({0})";
                object[] parameters = [userId];

                var orderId =
                    await _context.Database.SqlQueryRaw<int>(query, parameters).ToListAsync()
                    ?? throw new OperationFailedException("Cart Checkout", "Database Error");

                var order =
                    await _dbSet.SingleOrDefaultAsync(e => e.Id == orderId.FirstOrDefault())
                    ?? throw new EntityNotFoundException("Order", orderId);

                return order;
            }
            catch
            {
                throw new OperationFailedException("Cart Checkout", "Database Error");
            }
        }
    }

    public class IntResult
    {
        public int OrderId { get; set; }
    }
}
