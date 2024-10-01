using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Repository.OrderRepoAggregate;
using Ecommerce.Service.src.OrderServiceAggregate.OrderDetailAggregate.OrderDetailAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.OrderDetailAggregate;
using Ecommerce.Service.src.Shared.Implementation;

namespace Ecommerce.Service.src.OrderServiceAggregate.OrderDetailAggregate
{
    public class OrderDetailService(IOrderDetailRepo orderDetailRepo)
        : BaseService<
            OrderDetail,
            OrderDetailReadDto,
            OrderDetailUpdateDto,
            OrderDetailCreateDto,
            OrderDetailUpdateValidator,
            OrderDetailCreateValidator
        >(orderDetailRepo),
            IOrderDetailService
    {
        private readonly IOrderDetailRepo _repo = orderDetailRepo;
    }
}
