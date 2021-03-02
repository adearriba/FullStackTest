using FullStack.Models;
using FullStack.MVC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FullStack.MVC.Services
{
    public class BrandService : CRUDBaseService<Brand>, IBrandService
    {
        public BrandService(HttpClient httpClient)
            : base(httpClient, "brands")
        {

        }
    }
}
