using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStack.Models
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Marca")]
        public string Name { get; set; }

        [Display(Name = "Móviles")]
        public List<Mobile> Mobiles { get; set; } = new List<Mobile>();
    }
}
