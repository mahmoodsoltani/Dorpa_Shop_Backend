using System.ComponentModel.DataAnnotations;
using Ecommerce.Model.src.Entity.OrderAggregate;

namespace Ecommerce.Model.src.Entity.UserAggregate
{
    public class User : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public byte[] Salt { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public bool IsAdmin { get; set; } = false;

        [Required]
        public bool IsDeleted { get; set; } = false;

        // Navigation Properties (One-to-Many relationship)
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Favourite>? Favourites { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<CartDetail>? CartDetails { get; set; }
    }
}
