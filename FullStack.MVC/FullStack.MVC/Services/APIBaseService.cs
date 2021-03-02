using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FullStack.MVC.Services
{
    public abstract class APIBaseService
    {
        protected readonly HttpClient _httpClient;
        protected string _apiUri;

        public APIBaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiUri = Environment.GetEnvironmentVariable("ApiUri");
        }
    }
}
