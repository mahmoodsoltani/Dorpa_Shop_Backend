using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model.src.Entity.ProductAggregate
{
    public class Color : BaseEntity
    {
        [Required(ErrorMessage = "Color name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Color Code is required")]
        public string Code { get; set; }
    }
}
