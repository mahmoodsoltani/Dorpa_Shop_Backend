using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.DiscountAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.ProductAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller.ProductControllerAggregate
{
    public class DiscountController(IDiscountService discountService)
        : BaseController<
            Discount,
            DiscountReadDto,
            DiscountUpdateDto,
            DiscountCreateDto,
            DiscountUpdateValidator,
            DiscountCreateValidator
        >(discountService)
    {
        [AllowAnonymous]
        public override async Task<ActionResult<IEnumerable<DiscountReadDto>>> GetAllAsync(
            [FromQuery] QueryOptions queryOptions
        )
        {
            return await base.GetAllAsync(queryOptions);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<ActionResult<DiscountReadDto>> GetAsync(int id)
        {
            return await base.GetAsync(id);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public override async Task<ActionResult<DiscountReadDto>> CreateAsync(
            [FromBody] DiscountCreateDto createDto
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
            [FromBody] DiscountUpdateDto updateDto
        )
        {
            return await base.UpdateAsync(updateDto);
        }
    }
}
