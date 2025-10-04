using Microsoft.AspNetCore.Mvc;
using xtramile_server.Controllers;
using xtramile_server.Models;
using xtramile_server.Services;

namespace xtramile_server.Tests
{
    public class CountriesControllerTests
    {
        [Fact]
        public void GetCountriesReturnsOkWithCountries()
        {
            ICountryStore store = new FakeCountryStore(
                [new("US", "United States"), new("ID", "Indonesia")],
                [new("Jakarta", "ID")]
            );

            var controller = new CountriesController(store);
            var result = controller.GetCountries();

            var ok = Assert.IsType<OkObjectResult>(result);
            var countries = Assert.IsAssignableFrom<IEnumerable<Country>>(ok.Value);
            Assert.Equal(["US", "ID"], countries.Select(c => c.Code).ToArray());
        }

        [Fact]
        public void GetCitiesReturnsOkWhenFound()
        {
            ICountryStore store = new FakeCountryStore(
                [new("ID", "Indonesia")],
                [new("Jakarta", "ID"), new("Bandung", "ID")]
            );

            var controller = new CountriesController(store);
            var result = controller.GetCities("ID");

            var ok = Assert.IsType<OkObjectResult>(result);
            var cities = Assert.IsAssignableFrom<IEnumerable<City>>(ok.Value);
            Assert.Equal(["Jakarta", "Bandung"], cities.Select(c => c.Name).ToArray());
        }

        [Fact]
        public void GetCitiesReturnsNotFoundWhenEmpty()
        {
            ICountryStore store = new FakeCountryStore(
                [new("US", "United States")],
                [new("Jakarta", "ID")]
            );

            var controller = new CountriesController(store);
            var result = controller.GetCities("GB");

            Assert.IsType<NotFoundResult>(result);
        }

        private sealed class FakeCountryStore(List<Country> countries, List<City> cities) : ICountryStore
        {
            public List<Country> GetCountries() => countries;
            public List<City> GetCities(string countryCode) => [.. cities.Where(c => string.Equals(c.CountryCode, countryCode, StringComparison.OrdinalIgnoreCase))];
        }
    }

    public class WeatherControllerTests
    {
        [Fact]
        public async Task GetReturnsOkWhenWeatherExists()
        {
            var svc = new FakeWeatherService(new Weather(
                "Bandung", "Indonesia", DateTime.UtcNow,
                5.5, 180, 10000, "clear", 77, 25, 55, 60, 1013));

            var controller = new WeatherController(svc);
            var result = await controller.Get("Bandung");

            var ok = Assert.IsType<OkObjectResult>(result);
            var weather = Assert.IsType<Weather>(ok.Value);
            Assert.Equal("Bandung", weather.City);
            Assert.Equal(77, weather.TemperatureF);
            Assert.Equal(25, weather.TemperatureC);
        }

        [Fact]
        public async Task GetReturnsNotFoundWhenWeatherIsNull()
        {
            var controller = new WeatherController(new FakeWeatherService(null));
            var result = await controller.Get("Nowhere");

            Assert.IsType<NotFoundResult>(result);
        }

        private sealed class FakeWeatherService(Weather? weather) : IWeatherService
        {
            public Task<Weather?> GetWeatherAsync(string city) => Task.FromResult(weather);
        }
    }
}