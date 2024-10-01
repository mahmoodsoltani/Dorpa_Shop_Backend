using System.Linq.Expressions;
using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Repository.OrderRepoAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.ProductServiceAggregate.OrderAggregate;
using Ecommerce.Service.src.Shared.Implementation;

namespace Ecommerce.Service.src.OrderServiceAggregate.OrderAggregate
{
    public class OrderService(IOrderRepo orderRepo)
        : BaseService<
            Order,
            OrderReadDto,
            OrderUpdateDto,
            OrderCreateDto,
            OrderUpdateValidator,
            OrderCreateValidator
        >(orderRepo),
            IOrderService
    {
        private readonly IOrderRepo _repo = orderRepo;

        public async Task<OrderReadDto> CartCheckoutAsync(int userId)
        {
            var result = await _repo.CartCheckout(userId);
            OrderReadDto orderReadDto = new();
            orderReadDto.FromEntity(result);
            return orderReadDto;
        }
         public async Task<PaginatedResult<OrderReadDto>> GetAllAsync(
            QueryOptions queryOptions,
            Expression<Func<Order, bool>>? filter = null,
            Expression<Func<Order, object>>[] includes = null
        )
        {
            var result = await _repo.GetAllAsync(
                queryOptions,
                filter,
                [
                    o => o.OrderDetails,
                ],
                false
            );
            var convertedToReadDto = result.Items.Select(u =>
            {
                var readDto = Activator.CreateInstance<OrderReadDto>();
                readDto.FromEntity(u);
                return readDto;
            });
            return new PaginatedResult<OrderReadDto>
            {
                Items = convertedToReadDto,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }
    }
}
