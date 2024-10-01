using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model.src.Entity.ProductAggregate
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }

        // Collection of products in this category
        // public ICollection<Product>? Products { get; set; }
        public Category Parent { get; set; }
    }
}
