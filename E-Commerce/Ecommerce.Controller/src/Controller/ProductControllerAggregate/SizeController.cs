using System;
using System.Collections.Generic;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller.SizeControllerAggregate
{
    public class SizeController(ISizeService sizeService)
        : BaseController<
            Size,
            SizeReadDto,
            SizeUpdateDto,
            SizeCreateDto,
            SizeUpdateValidator,
            SizeCreateValidator
        >(sizeService)
    {
        [AllowAnonymous]
        public override async Task<ActionResult<IEnumerable<SizeReadDto>>> GetAllAsync(
            [FromQuery] QueryOptions queryOptions
        )
        {
            return await base.GetAllAsync(queryOptions);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<ActionResult<SizeReadDto>> GetAsync(int id)
        {
            return await base.GetAsync(id);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public override async Task<ActionResult<SizeReadDto>> CreateAsync(
            [FromBody] SizeCreateDto createDto
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
        public override async Task<IActionResult> UpdateAsync([FromBody] SizeUpdateDto updateDto)
        {
            return await base.UpdateAsync(updateDto);
        }
    }
}
