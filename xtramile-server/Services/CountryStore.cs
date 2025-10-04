using xtramile_server.Models;

namespace xtramile_server.Services
{
    public interface ICountryStore
    {
        List<Country> GetCountries();
        List<City> GetCities(string countryCode);
    }

    public class CountryStore : ICountryStore
    {
        private readonly List<Country> _countries =
        [
            new("US", "United States"),
            new("ID", "Indonesia"),
            new("GB", "United Kingdom"),
            new("JP", "Japan")
        ];

        private readonly List<City> _cities =
        [
            new("New York", "US"),
            new("Los Angeles", "US"),
            new("Jakarta", "ID"),
            new("Bandung", "ID"),
            new("London", "GB"),
            new("Tokyo", "JP")
        ];

        public List<Country> GetCountries() => _countries;

        public List<City> GetCities(string countryCode) => [.. _cities.Where(c => string.Equals(c.CountryCode, countryCode, StringComparison.OrdinalIgnoreCase))];
    }
}