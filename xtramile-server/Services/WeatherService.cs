using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using xtramile_server.Models;

namespace xtramile_server.Services
{
    public interface IWeatherService
    {
        Task<Weather?> GetWeatherAsync(string city);
    }

    public class WeatherService(
        HttpClient httpClient,
        ICountryStore countryStore,
        ITemperatureConverter temperatureConverter,
        IOptions<AppSettings> options
    ) : IWeatherService
    {
        private readonly AppSettings _settings = options.Value;

        public async Task<Weather?> GetWeatherAsync(string city)
        {
            var now = DateTime.UtcNow;
            if (string.IsNullOrWhiteSpace(_settings.OpenWeather.ApiKey)) return CreateMockWeather(city, now);

            var requestUrl = $"{_settings.OpenWeather.BaseUrl}/data/2.5/weather?q={Uri.EscapeDataString(city)}&appid={_settings.OpenWeather.ApiKey}&units=imperial";
            var httpResponse = await httpClient.GetAsync(requestUrl);
            if (!httpResponse.IsSuccessStatusCode) return CreateMockWeather(city, now);

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<OpenWeatherResponse>(jsonResponse);
            if (response is null) return CreateMockWeather(city, now);

            var countryCode = response.Sys.Country ?? "";
            var countryName = countryStore.GetCountries().FirstOrDefault(c => c.Code == countryCode)?.Name ?? countryCode;
            var fahrenheit = response.Main.Temp ?? 0d;
            var celsius = temperatureConverter.FahrenheitToCelsius(fahrenheit);

            return new Weather(
                city,
                countryName,
                now,
                response.Wind.Speed ?? 0,
                response.Wind.Deg ?? 0,
                response.Visibility,
                response.Weather.FirstOrDefault()?.Main ?? "unknown",
                fahrenheit,
                celsius,
                response.Main.DewPoint ?? 0,
                response.Main.Humidity ?? 0,
                response.Main.Pressure ?? 0
            );
        }

        private Weather CreateMockWeather(string city, DateTime now)
        {
            var firstCountryName = countryStore.GetCountries().First().Name;
            var mockFahrenheit = 77d;

            return new Weather(
                city,
                firstCountryName,
                now,
                5.5,
                180,
                10000,
                "clear",
                mockFahrenheit,
                temperatureConverter.FahrenheitToCelsius(mockFahrenheit),
                55,
                60,
                1013
            );
        }
    }
}