using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Shared;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate
{
    public class ColorReadDto : BaseReadDto<Color>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public override void FromEntity(Color entity)
        {
            if (entity == null)
                return;
            Name = entity.Name;
            Code = entity.Code;
            base.FromEntity(entity);
        }
    }

    public class ColorCreateDto : ICreateDto<Color>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public void ToEntity(Color entity)
        {
            entity.Name = Name;
            entity.Code = Code;
            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class ColorUpdateDto : IUpdateDto<Color>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

        public void UpdateEntity(Color entity)
        {
            entity.Name = Name ?? entity.Name;
            entity.Code = Code ?? entity.Code;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class ColorUpdateValidator : IDataValidator<ColorUpdateDto>
    {
        public ColorUpdateValidator() { }
    }

    public class ColorCreateValidator : IDataValidator<ColorCreateDto>
    {
        public ColorCreateValidator() { }
    }
}
