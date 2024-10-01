using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Repository.ProductRepoAggreate;
using Ecommerce.Service.src.Shared.Implementation;

namespace Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate
{
    public class SizeService
        : BaseService<
            Size,
            SizeReadDto,
            SizeUpdateDto,
            SizeCreateDto,
            SizeUpdateValidator,
            SizeCreateValidator
        >,
            ISizeService
    {
        public SizeService(ISizeRepo colorRepo)
            : base(colorRepo) { }
    }
}
