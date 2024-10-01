using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Model.src
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DefaultValue("CURRENT_TIMESTAMP")]
        public DateTime Create_Date { get; set; } = DateTime.UtcNow;

        [DefaultValue("CURRENT_TIMESTAMP")]
        public DateTime Update_Date { get; set; } = DateTime.UtcNow;
    }
}
