using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Repository.ProductRepoAggreate;
using Ecommerce.Service.src.Shared.Implementation;

namespace Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate
{
    public class ColorService
        : BaseService<
            Color,
            ColorReadDto,
            ColorUpdateDto,
            ColorCreateDto,
            ColorUpdateValidator,
            ColorCreateValidator
        >,
            IColorService
    {
        public ColorService(IColorRepo colorRepo)
            : base(colorRepo) { }
    }
}
