using System.Linq.Expressions;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.ProductRepoAggreate;
using Ecommerce.Service.src.ProductServiceAggregate.CategoryAggregate;
using Ecommerce.Service.src.Shared.Implementation;

namespace Ecommerce.Service.src.CategoryServiceAggregate.CategoryAggregate
{
    public class CategoryService
        : BaseService<
            Category,
            CategoryReadDto,
            CategoryUpdateDto,
            CategoryCreateDto,
            CategoryUpdateValidator,
            CategoryCreateValidator
        >,
            ICategoryService
    {
        private ICategoryRepo _categoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
            : base(categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        // public async Task<string> GenerateCategoryPath(int categoryId)
        // {
        //     var categories = new List<string>();

        //     Category category =
        //         await _categoryRepo.GetAsync(c => c.Id == categoryId, [c => c.Parent], false)
        //         ?? throw new EntityNotFoundException("Category", categoryId);

        //     while (category != null)
        //     {
        //         categories.Insert(0, category.Name);

        //         category = category.Parent;
        //     }
        //     return Path.Combine([.. categories]);
        // }
    }
}
