using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Service.src.ProductServiceAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.Dto;
using Ecommerce.Service.src.ProductServiceAggregate.ProductAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller.ProductControllerAggregate
{
    public class ProductImageController(IProductImageService productImageService)
        : BaseController<
            ProductImage,
            ProductImageReadDto,
            ProductImageUpdateDto,
            ProductImageCreateDto,
            ProductImageUpdateValidator,
            ProductImageCreateValidator
        >(productImageService)
    {
        private readonly IProductImageService _productImageService = productImageService;

        [HttpPost]
        public override async Task<ActionResult<ProductImageReadDto>> CreateAsync(
            [FromForm] ProductImageCreateDto createDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var readDto = await _productImageService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetAsync), new { id = readDto.Id }, readDto);
        }
    }
}
