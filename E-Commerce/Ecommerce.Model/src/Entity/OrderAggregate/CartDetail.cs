using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Entity.UserAggregate;

namespace Ecommerce.Model.src.Entity.OrderAggregate
{
    public class CartDetail : BaseEntity
    {
        [Required(ErrorMessage = "Product quantity is required")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Quantity must be larger than to 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
