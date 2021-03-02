using FullStack.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace FullStack.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Index()
        {
            var weather = await _weatherService.GetWeatherDataAsync();
            return Ok(weather);
        }
    }
}
