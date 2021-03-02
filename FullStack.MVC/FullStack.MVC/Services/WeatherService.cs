using FullStack.Models;
using FullStack.MVC.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FullStack.MVC.Services
{
    public class WeatherService : APIBaseService, IWeatherService
    {
        public WeatherService(HttpClient httpClient)
            : base(httpClient)
        {
        }

        public async Task<WeatherData> GetWeatherDataAsync()
        {
            var responseString = await _httpClient.GetStringAsync(_apiUri);
            var data = JsonConvert.DeserializeObject<WeatherData>(responseString);

            return data;
        }
    }
}
