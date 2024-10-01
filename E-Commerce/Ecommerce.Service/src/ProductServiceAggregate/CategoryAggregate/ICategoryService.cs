using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.CategoryAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Interface;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.src.CategoryServiceAggregate.CategoryAggregate
{
    public interface ICategoryService
        : IBaseService<
            Category,
            CategoryReadDto,
            CategoryUpdateDto,
            CategoryCreateDto,
            CategoryUpdateValidator,
            CategoryCreateValidator
        >
    {
        // Task<string> GenerateCategoryPath(int categoryId);
    }
}
