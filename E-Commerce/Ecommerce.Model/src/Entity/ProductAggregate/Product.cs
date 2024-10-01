using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ecommerce.Model.src.Entity;
using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Entity.UserAggregate;

namespace Ecommerce.Model.src.Entity.ProductAggregate
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        [Required]
        [Range(0, 9999999.99, ErrorMessage = "Price must be greater than or equal to 0")]
        public int Stock { get; set; }

        public int? SizeId { get; set; }

        public int? ColorId { get; set; }

        [Required]
        [Range(0, 9999999.99, ErrorMessage = "Price must be greater than or equal to 0")]
        public decimal Price { get; set; }

        // Nav
        public Brand? Brand { get; set; }
        public Size? Size { get; set; }
        public Color? Color { get; set; }
        public Category? Category { get; set; } 
        public Discount? Discount { get; set; }

        //Collections
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<CartDetail>? CartDetails { get; set; }
        public ICollection<Favourite>? Favourites { get; set; }
    }
}
