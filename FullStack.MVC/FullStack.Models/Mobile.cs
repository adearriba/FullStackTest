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

        [Display(Name = "Descripción")]
        [Column(TypeName = "VARCHAR(250)")]
        public string Description { get; set; }

        [Display(Name = "Cámara")]
        [Column(TypeName = "VARCHAR(100)")]
        public string CamaraDescripcion { get; set; }

        [Display(Name = "Almacenamiento")]
        [Column(TypeName = "VARCHAR(100)")]
        public string StorageDescription { get; set; }

        [Display(Name = "Pantalla")]
        [Column(TypeName = "VARCHAR(100)")]
        public string ScreenDescription { get; set; }

        [Display(Name = "Batería")]
        [Column(TypeName = "VARCHAR(100)")]
        public string BateryDescription { get; set; }
    }
}
