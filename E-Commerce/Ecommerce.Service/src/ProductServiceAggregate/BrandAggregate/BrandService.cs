using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Repository.ProductRepoAggreate;
using Ecommerce.Service.src.Shared.Implementation;

namespace Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate
{
    public class BrandService
        : BaseService<
            Brand,
            BrandReadDto,
            BrandUpdateDto,
            BrandCreateDto,
            BrandUpdateValidator,
            BrandCreateValidator
        >,
            IBrandService
    {
        public BrandService(IBrandRepo brandRepo)
            : base(brandRepo) { }
    }
}
