using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Shared;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate
{
    public class BrandReadDto : BaseReadDto<Brand>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? AltText { get; set; }

        public override void FromEntity(Brand entity)
        {
            if (entity == null)
                return;
            Name = entity.Name;
            Description = entity.Description;
            ImageUrl = entity.ImageUrl;
            AltText = entity.AltText;
            base.FromEntity(entity);
        }
    }

    public class BrandCreateDto : ICreateDto<Brand>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? AltText { get; set; }

        public void ToEntity(Brand entity)
        {
            entity.Name = Name;
            entity.Description = Description;
            entity.ImageUrl = ImageUrl;
            entity.AltText = AltText;
            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class BrandUpdateDto : IUpdateDto<Brand>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? AltText { get; set; }

        public void UpdateEntity(Brand entity)
        {
            entity.Name = Name ?? entity.Name;
            entity.Description = Description ?? entity.Description;
            entity.ImageUrl = ImageUrl ?? entity.ImageUrl;
            entity.AltText = AltText ?? entity.AltText;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class BrandUpdateValidator : IDataValidator<BrandUpdateDto>
    {
        public BrandUpdateValidator() { }
    }

    public class BrandCreateValidator : IDataValidator<BrandCreateDto>
    {
        public BrandCreateValidator() { }
    }
}
