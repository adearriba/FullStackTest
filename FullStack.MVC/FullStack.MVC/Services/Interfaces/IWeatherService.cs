using FullStack.Models;
using System.Threading.Tasks;

namespace FullStack.MVC.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherDataAsync();
    }
}
