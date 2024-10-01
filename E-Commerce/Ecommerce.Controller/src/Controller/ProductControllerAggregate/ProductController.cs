using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.ProductServiceAggregate.ProductAggregate;
using Ecommerce.Service.src.Shared.ImageServiceAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller.ProductControllerAggregate
{
    public class ProductController(IProductService productService)
        : BaseController<
            Product,
            ProductReadDto,
            ProductUpdateDto,
            ProductCreateDto,
            ProductUpdateValidator,
            ProductCreateValidator
        >(productService)
    {
        private readonly IProductService _productService = productService;

        [AllowAnonymous]
        public override async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllAsync(
            [FromQuery] QueryOptions queryOptions
        )
        {
            return await base.GetAllAsync(queryOptions);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<ActionResult<ProductReadDto>> GetAsync(int id)
        {
            return await base.GetAsync(id);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public override async Task<ActionResult<ProductReadDto>> CreateAsync(
            [FromForm] ProductCreateDto createDto
        )
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidInputDataException(createDto.Name);
            }

            var readDto = await _productService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetAsync), new { id = readDto.Id }, readDto);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut]
        public override async Task<IActionResult> UpdateAsync([FromBody] ProductUpdateDto updateDto)
        {
            return await base.UpdateAsync(updateDto);
        }
    }
}
