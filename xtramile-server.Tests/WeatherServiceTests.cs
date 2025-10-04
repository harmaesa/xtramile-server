using System.Net;
using Microsoft.Extensions.Options;
using xtramile_server.Models;
using xtramile_server.Services;

namespace xtramile_server.Tests
{
    public class WeatherServiceTests
    {
        [Fact]
        public async Task UsesApiWhenOk()
        {
            var json = """
            {
              "sys": { "country": "US" },
              "main": { "temp": 77, "humidity": 60, "pressure": 1013, "dew_point": 55 },
              "wind": { "speed": 5.5, "deg": 180 },
              "weather": [ { "main": "clear" } ],
              "visibility": 10000
            }
            """;

            var http = new HttpClient(new StubHandler(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json)
            }));

            var store = new TestCountryStore(
            [
                new Country("US", "United States"),
                new Country("ID", "Indonesia")
            ]);

            var svc = new WeatherService(
                http,
                store,
                new TemperatureConverter(),
                Options.Create(new AppSettings { OpenWeather = new OpenWeatherSettings { BaseUrl = "https://api.openweathermap.org", ApiKey = "KEY" } })
            );

            var result = await svc.GetWeatherAsync("Bandung");

            Assert.NotNull(result);
            Assert.Equal("Bandung", result!.City);
            Assert.Equal("United States", result.Country);
            Assert.Equal(77, result.TemperatureF);
            Assert.Equal(25, result.TemperatureC);
            Assert.Equal("clear", result.Sky);
            Assert.Equal(10000, result.Visibility);
            Assert.Equal(5.5, result.WindSpeed);
            Assert.Equal(180, result.WindDirection);
            Assert.Equal(55, result.DewPoint);
            Assert.Equal(60, result.Humidity);
            Assert.Equal(1013, result.Pressure);
        }

        [Fact]
        public async Task ReturnsMockWhenApiKeyMissing()
        {
            var http = new HttpClient(new StubHandler(new HttpResponseMessage(HttpStatusCode.Unauthorized)));

            var store = new TestCountryStore(
            [
                new Country("ID", "Indonesia"),
                new Country("US", "United States")
            ]);

            var svc = new WeatherService(
                http,
                store,
                new TemperatureConverter(),
                Options.Create(new AppSettings { OpenWeather = new OpenWeatherSettings { BaseUrl = "https://api.openweathermap.org", ApiKey = "" } })
            );

            var result = await svc.GetWeatherAsync("Bandung");

            Assert.NotNull(result);
            Assert.Equal("Indonesia", result!.Country);
            Assert.Equal(77, result.TemperatureF);
            Assert.Equal(25, result.TemperatureC);
            Assert.Equal("clear", result.Sky);
        }

        [Fact]
        public async Task ReturnsMockOnNonSuccessStatus()
        {
            var http = new HttpClient(new StubHandler(new HttpResponseMessage(HttpStatusCode.Unauthorized)));

            var store = new TestCountryStore(
            [
                new Country("US", "United States")
            ]);

            var svc = new WeatherService(
                http,
                store,
                new TemperatureConverter(),
                Options.Create(new AppSettings { OpenWeather = new OpenWeatherSettings { BaseUrl = "https://api.openweathermap.org", ApiKey = "KEY" } })
            );

            var result = await svc.GetWeatherAsync("Bandung");

            Assert.NotNull(result);
            Assert.Equal(77, result!.TemperatureF);
            Assert.Equal(25, result.TemperatureC);
            Assert.Equal("clear", result.Sky);
        }

        [Fact]
        public async Task ReturnsMockWhenJsonIsNull()
        {
            var http = new HttpClient(new StubHandler(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("null")
            }));

            var store = new TestCountryStore(
            [
                new Country("US", "United States")
            ]);

            var svc = new WeatherService(
                http,
                store,
                new TemperatureConverter(),
                Options.Create(new AppSettings { OpenWeather = new OpenWeatherSettings { BaseUrl = "https://api.openweathermap.org", ApiKey = "KEY" } })
            );

            var result = await svc.GetWeatherAsync("Bandung");

            Assert.NotNull(result);
            Assert.Equal(77, result!.TemperatureF);
            Assert.Equal(25, result.TemperatureC);
        }

        private sealed class StubHandler(HttpResponseMessage response) : HttpMessageHandler
        {
            private readonly HttpResponseMessage _response = response;

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => Task.FromResult(_response);
        }

        private sealed class TestCountryStore(List<Country> countries) : ICountryStore
        {
            private readonly List<Country> _countries = countries;
            private readonly List<City> _cities = [];

            public List<Country> GetCountries() => _countries;

            public List<City> GetCities(string countryCode) => _cities;
        }
    }
}