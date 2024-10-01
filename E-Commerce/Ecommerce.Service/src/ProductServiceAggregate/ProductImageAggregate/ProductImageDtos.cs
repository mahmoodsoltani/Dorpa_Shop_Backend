using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Shared;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.src.ProductServiceAggregate.Dto
{
    public class ProductImageReadDto : BaseReadDto<ProductImage>
    {
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
        public string AltText { get; set; }
        public int ProductId { get; set; }

        public override void FromEntity(ProductImage entity)
        {
            ImageUrl = entity.ImageUrl;
            IsPrimary = entity.IsPrimary;
            AltText = entity.AltText;
            ProductId = entity.ProductId;
            base.FromEntity(entity);
        }
    }

    public class ProductImageCreateDto : ICreateDto<ProductImage>
    {
        public int ProductId { get; set; }
        public IFormFile Image { get; set; }
        public bool? IsPrimary { get; set; } = false;
        public string? AltText { get; set; } = "Image";

        public void ToEntity(ProductImage entity)
        {
            entity.ProductId = ProductId;
            entity.IsPrimary = IsPrimary ?? false;
            entity.AltText = AltText;
            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class ProductImageUpdateDto : IUpdateDto<ProductImage>
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsPrimary { get; set; }
        public string? AltText { get; set; }

        public void UpdateEntity(ProductImage entity)
        {
            entity.IsPrimary = IsPrimary ?? entity.IsPrimary;
            entity.AltText = AltText ?? entity.AltText;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class ProductImageUpdateValidator : IDataValidator<ProductImageUpdateDto>
    {
        public ProductImageUpdateValidator() { }
    }

    // Validator for ProductImageCreateDto
    public class ProductImageCreateValidator : IDataValidator<ProductImageCreateDto>
    {
        public ProductImageCreateValidator() { }
    }
}
