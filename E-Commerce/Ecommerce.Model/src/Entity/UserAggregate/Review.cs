using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Model.src.Entity.ProductAggregate;

namespace Ecommerce.Model.src.Entity.UserAggregate
{
    public class Review : BaseEntity
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime Review_Date { get; set; } = DateTime.UtcNow;

        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }

        // Foreign Key to User
        public int UserId { get; set; }

        // Foreign Key to Product
        public int ProductId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
