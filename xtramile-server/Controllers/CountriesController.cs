using Microsoft.AspNetCore.Mvc;
using xtramile_server.Services;

namespace xtramile_server.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class CountriesController(ICountryStore countryStore) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCountries() => Ok(countryStore.GetCountries());

        [HttpGet("{countryCode}/cities")]
        public IActionResult GetCities(string countryCode)
        {
            var cities = countryStore.GetCities(countryCode);
            return cities.Count == 0 ? NotFound() : Ok(cities);
        }
    }
}