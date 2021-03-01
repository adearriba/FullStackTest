using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FullStack.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WeatherController : Controller
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
