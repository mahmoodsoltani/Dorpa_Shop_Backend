using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.Dto;
using Ecommerce.Service.src.ProductServiceAggregate.ProductAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.ProductServiceAggregate.ProductAggregate
{
    public interface IProductService
        : IBaseService<
            Product,
            ProductReadDto,
            ProductUpdateDto,
            ProductCreateDto,
            ProductUpdateValidator,
            ProductCreateValidator
        > { }
}
