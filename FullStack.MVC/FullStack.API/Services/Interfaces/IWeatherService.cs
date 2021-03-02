using FullStack.Models;
using System.Threading.Tasks;

namespace FullStack.API.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherDataAsync();
    }
}
