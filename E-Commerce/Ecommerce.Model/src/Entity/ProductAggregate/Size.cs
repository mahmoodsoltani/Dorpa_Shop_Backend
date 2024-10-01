using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model.src.Entity.ProductAggregate
{
    public class Size : BaseEntity
    {
        [Required(ErrorMessage = "Size name is required")]
        public string Name { get; set; }
    }
}
