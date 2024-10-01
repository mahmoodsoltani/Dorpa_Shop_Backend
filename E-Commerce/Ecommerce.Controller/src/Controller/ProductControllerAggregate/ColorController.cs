using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller.ProductControllerAggregate
{
    public class ColorController(IColorService colorService)
        : BaseController<
            Color,
            ColorReadDto,
            ColorUpdateDto,
            ColorCreateDto,
            ColorUpdateValidator,
            ColorCreateValidator
        >(colorService) {
             [AllowAnonymous]
        public override async Task<ActionResult<IEnumerable<ColorReadDto>>> GetAllAsync(
            [FromQuery] QueryOptions queryOptions
        )
        {
            return await base.GetAllAsync(queryOptions);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<ActionResult<ColorReadDto>> GetAsync(int id)
        {
            return await base.GetAsync(id);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public override async Task<ActionResult<ColorReadDto>> CreateAsync(
            [FromForm] ColorCreateDto createDto
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
        public override async Task<IActionResult> UpdateAsync([FromBody] ColorUpdateDto updateDto)
        {
            return await base.UpdateAsync(updateDto);
        }
         }
}
