using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Service.src.OrderServiceAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.Dto;
using Ecommerce.Service.src.ProductServiceAggregate.OrderDetailAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.OrderServiceAggregate.OrderDetailAggregate.OrderDetailAggregate
{
    public interface IOrderDetailService
        : IBaseService<
            OrderDetail,
            OrderDetailReadDto,
            OrderDetailUpdateDto,
            OrderDetailCreateDto,
            OrderDetailUpdateValidator,
            OrderDetailCreateValidator
        > { }
}
