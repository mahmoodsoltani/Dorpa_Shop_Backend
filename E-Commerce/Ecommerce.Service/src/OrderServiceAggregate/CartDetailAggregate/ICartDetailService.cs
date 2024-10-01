using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.OrderServiceAggregate.CartDetailAggregate
{
    public interface ICartDetailService
        : IBaseService<
            CartDetail,
            CartDetailReadDto,
            CartDetailUpdateDto,
            CartDetailCreateDto,
            CartDetailUpdateValidator,
            CartDetailCreateValidator
        > { }
}
