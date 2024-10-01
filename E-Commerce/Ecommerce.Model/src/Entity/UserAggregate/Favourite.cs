using Ecommerce.Model.src.Entity.ProductAggregate;

namespace Ecommerce.Model.src.Entity.UserAggregate
{
    public class Favourite : BaseEntity
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
