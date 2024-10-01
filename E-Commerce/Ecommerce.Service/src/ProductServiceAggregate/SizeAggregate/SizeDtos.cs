using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Shared;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate
{
    public class SizeReadDto : BaseReadDto<Size>
    {
        public string Name { get; set; }

        public override void FromEntity(Size entity)
        {
            if (entity == null)
                return;
            Name = entity.Name;
            base.FromEntity(entity);
        }
    }

    public class SizeCreateDto : ICreateDto<Size>
    {
        public string Name { get; set; }

        public void ToEntity(Size entity)
        {
            entity.Name = Name;
            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class SizeUpdateDto : IUpdateDto<Size>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public void UpdateEntity(Size entity)
        {
            entity.Name = Name ?? entity.Name;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class SizeUpdateValidator : IDataValidator<SizeUpdateDto>
    {
        public SizeUpdateValidator() { }
    }

    public class SizeCreateValidator : IDataValidator<SizeCreateDto>
    {
        public SizeCreateValidator() { }
    }
}
