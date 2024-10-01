using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model.src.Entity.ProductAggregate
{
    public class Brand : BaseEntity
    {
        [Required(ErrorMessage = "Brand name is required")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? AltText { get; set; } = "Brand Icon";

        // Collection of products related with the brand
        // public ICollection<Product>? Products { get; set; }
    }
}
