using System;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Shared;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.ProductServiceAggregate.CategoryAggregate
{
    public class CategoryReadDto : BaseReadDto<Category>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public string? ParentName { get; set; }

        public override void FromEntity(Category entity)
        {
            Name = entity.Name;
            Description = entity.Description;
            ParentId = entity.ParentId;
            ParentName = entity.Parent?.Name;
            base.FromEntity(entity);
        }
    }

    public class CategoryCreateDto : ICreateDto<Category>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }

        public void ToEntity(Category entity)
        {
            entity.Name = Name;
            entity.Description = Description;
            entity.ParentId = ParentId;
            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class CategoryUpdateDto : IUpdateDto<Category>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }

        public void UpdateEntity(Category entity)
        {
            entity.Name = Name ?? entity.Name;
            entity.Description = Description ?? entity.Description;
            entity.ParentId = ParentId ?? entity.ParentId;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class CategoryUpdateValidator : IDataValidator<CategoryUpdateDto>
    {
        public CategoryUpdateValidator() { }
    }

    // Validator for CategoryCreateDto
    public class CategoryCreateValidator : IDataValidator<CategoryCreateDto>
    {
        public CategoryCreateValidator() { }
    }
}
