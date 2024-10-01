using System.ComponentModel.DataAnnotations;
using Ecommerce.Model.src.Entity;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Entity.UserAggregate;

namespace Ecommerce.Model.src.Entity.OrderAggregate
{
    public class OrderDetail : BaseEntity
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product price is required")]
        [Range(0, 9999999.99, ErrorMessage = "Price must be greater than or equal to 0")]
        public decimal Price { get; set; }

        public decimal? Discount { get; set; }
        [Required(ErrorMessage = "Product stock is required")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Stock must be larger than or equal to 0")]
        public int Quantity { get; set; }

        // Navigation Properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}

