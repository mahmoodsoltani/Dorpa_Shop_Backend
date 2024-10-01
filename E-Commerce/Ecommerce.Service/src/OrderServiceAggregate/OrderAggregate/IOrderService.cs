using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Service.src.OrderServiceAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.Dto;
using Ecommerce.Service.src.ProductServiceAggregate.OrderAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.OrderServiceAggregate.OrderAggregate
{
    public interface IOrderService
        : IBaseService<
            Order,
            OrderReadDto,
            OrderUpdateDto,
            OrderCreateDto,
            OrderUpdateValidator,
            OrderCreateValidator
        >
    {
        Task<OrderReadDto> CartCheckoutAsync(int userId);
    }
}
