using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model.src.Entity.UserAggregate
{
    public class Address : BaseEntity
    {
        [Required]
        public string Street { get; set; }

        [Required]
        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }

        public int UserId { get; set; }

        // Navigation Property to User
        public User User { get; set; }

        public override string ToString()
        {
            return $"{Street}, {City}, {State}, {PostalCode}";
        }
    }
}
