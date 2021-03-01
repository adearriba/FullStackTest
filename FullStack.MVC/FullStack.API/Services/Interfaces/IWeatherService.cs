using FullStack.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.API.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherDataAsync();
    }
}
