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
        public List<Brand> Brands { get; set; }
}
}
