using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.CategoryServiceAggregate.CategoryAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.CategoryAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller.ProductControllerAggregate
{
    public class CategoryController(ICategoryService categoryService)
        : BaseController<
            Category,
            CategoryReadDto,
            CategoryUpdateDto,
            CategoryCreateDto,
            CategoryUpdateValidator,
            CategoryCreateValidator
        >(categoryService)
    {
        [AllowAnonymous]
        public override async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAllAsync(
            [FromQuery] QueryOptions queryOptions
        )
        {
            return await base.GetAllAsync(queryOptions);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<ActionResult<CategoryReadDto>> GetAsync(int id)
        {
            return await base.GetAsync(id);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public override async Task<ActionResult<CategoryReadDto>> CreateAsync(
            [FromForm] CategoryCreateDto createDto
        )
        {
            return await base.CreateAsync(createDto);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut]
        public override async Task<IActionResult> UpdateAsync(
            [FromBody] CategoryUpdateDto updateDto
        )
        {
            return await base.UpdateAsync(updateDto);
        }
    }
}
