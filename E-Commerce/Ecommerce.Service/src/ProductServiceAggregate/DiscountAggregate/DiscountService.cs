using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Repository.ProductRepoAggreate;
using Ecommerce.Service.src.Shared.Implementation;

namespace Ecommerce.Service.src.ProductServiceAggregate.DiscountAggregate
{
    public class DiscountService
        : BaseService<
            Discount,
            DiscountReadDto,
            DiscountUpdateDto,
            DiscountCreateDto,
            DiscountUpdateValidator,
            DiscountCreateValidator
        >,
            IDiscountService
    {
        public DiscountService(IDiscountRepo discountRepo)
            : base(discountRepo) { }
    }
}
