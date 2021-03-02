using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStack.Models
{
    public class Mobile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Modelo")]
        public string Model { get; set; }

        public int BrandId { get; set; }

        [Display(Name = "Marca")]
        public Brand Brand { get; set; }
    }
}
