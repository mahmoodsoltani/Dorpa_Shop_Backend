using System.ComponentModel.DataAnnotations;
using Ecommerce.Model.src.Entity;

namespace Ecommerce.Model.src.Entity.ProductAggregate
{
    public class ProductImage : BaseEntity
    {
        // FK the Product entity
        public int ProductId { get; set; }

        // Navigation property for the Product entity

        [Required(ErrorMessage = "Image URL is required")]
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; } = false;
        public string? AltText { get; set; } = "Product Image";

        public Product Product { get; set; }
    }
}
