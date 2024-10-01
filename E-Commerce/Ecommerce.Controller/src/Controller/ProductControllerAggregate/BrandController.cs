using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller.ProductControllerAggregate
{
    public class BrandController(IBrandService brandService)
        : BaseController<
            Brand,
            BrandReadDto,
            BrandUpdateDto,
            BrandCreateDto,
            BrandUpdateValidator,
            BrandCreateValidator
        >(brandService)
    {
        [AllowAnonymous]
        public override async Task<ActionResult<IEnumerable<BrandReadDto>>> GetAllAsync(
            [FromQuery] QueryOptions queryOptions
        )
        {
            return await base.GetAllAsync(queryOptions);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<ActionResult<BrandReadDto>> GetAsync(int id)
        {
            return await base.GetAsync(id);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public override async Task<ActionResult<BrandReadDto>> CreateAsync(
            [FromForm] BrandCreateDto createDto
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
        public override async Task<IActionResult> UpdateAsync([FromBody] BrandUpdateDto updateDto)
        {
            return await base.UpdateAsync(updateDto);
        }
    }
}
