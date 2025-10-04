using Microsoft.AspNetCore.Mvc;
using xtramile_server.Models;
using xtramile_server.Services;

namespace xtramile_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController(IWeatherService weather) : ControllerBase
    {
        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            var result = await weather.GetWeatherAsync(city);
            return result is null ? NotFound() : Ok(result);
        }
    }
}