using FullStack.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.MVC.Models.Mobile
{
    public class CreateMobileViewModel
    {
        [Display(Name = "Modelo")]
        public string Model { get; set; }

        [Display(Name = "Marca")]
        public int BrandId { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Cámara")]
        public string CamaraDescripcion { get; set; }

        [Display(Name = "Almacenamiento")]
        public string StorageDescription { get; set; }

        [Display(Name = "Pantalla")]
        public string ScreenDescription { get; set; }

        [Display(Name = "Batería")]
        public string BateryDescription { get; set; }


        public List<Brand> Brands { get; set; }
}
}
