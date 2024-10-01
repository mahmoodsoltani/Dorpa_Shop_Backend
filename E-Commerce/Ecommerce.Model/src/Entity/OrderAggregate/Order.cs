using System.ComponentModel.DataAnnotations;
using Ecommerce.Model.src.Entity;
using Ecommerce.Model.src.Entity.UserAggregate;

namespace Ecommerce.Model.src.Entity.OrderAggregate
{
    public class Order : BaseEntity
    {
        [Required]
        public DateTime OrderDate { get; set; }

        public decimal? Total { get; set; }
        public decimal? Discount { get; set; }

        [Required]
        public int UserId { get; set; }

        // Navigation Properties
        public User User { get; set; }

        // OrderDetails (One-to-Many Relationship with OrderDetail)
        public ICollection<OrderDetail>? OrderDetails { get; set; } = [];
    }
}
