using System.Linq.Expressions;
using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Repository.OrderRepoAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;

namespace Ecommerce.Service.src.OrderServiceAggregate.CartDetailAggregate
{
    public class CartDetailService(ICartDetailRepo CartDetailRepo)
        : BaseService<
            CartDetail,
            CartDetailReadDto,
            CartDetailUpdateDto,
            CartDetailCreateDto,
            CartDetailUpdateValidator,
            CartDetailCreateValidator
        >(CartDetailRepo),
            ICartDetailService
    {
        private readonly ICartDetailRepo _repo = CartDetailRepo;
    }
}
