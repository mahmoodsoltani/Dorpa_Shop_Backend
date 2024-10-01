using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Shared.ValueObject;

namespace Ecommerce.Model.src.Repository.OrderRepoAggregate
{
    public interface IOrderRepo : IBaseRepo<Order>
    {
        Task<Order> CartCheckout(int userId);
    }
}
