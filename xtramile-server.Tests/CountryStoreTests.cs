using Microsoft.Extensions.DependencyInjection;
using xtramile_server.Services;

namespace xtramile_server.Tests
{
    public class CountryStoreTests
    {
        [Fact]
        public void GetCountriesReturnsExpectedCountriesInOrder()
        {
            var store = new CountryStore();
            var countries = store.GetCountries();

            Assert.Equal(4, countries.Count);
            Assert.Equal(["US", "ID", "GB", "JP"], countries.Select(c => c.Code).ToArray());
            Assert.Equal(["United States", "Indonesia", "United Kingdom", "Japan"], countries.Select(c => c.Name).ToArray());
        }

        [Fact]
        public void GetCitiesFiltersByCountryCodeCaseInsensitive()
        {
            var store = new CountryStore();
            var citiesUpper = store.GetCities("ID");
            var citiesLower = store.GetCities("id");

            Assert.Equal(2, citiesUpper.Count);
            Assert.Contains(citiesUpper, c => c.Name == "Jakarta" && c.CountryCode == "ID");
            Assert.Contains(citiesUpper, c => c.Name == "Bandung" && c.CountryCode == "ID");
            Assert.Equal(citiesUpper.Select(c => c.Name), citiesLower.Select(c => c.Name));
        }

        [Fact]
        public void GetCitiesReturnsEmptyForUnknownCountryCode()
        {
            var store = new CountryStore();
            var cities = store.GetCities("ZZ");

            Assert.Empty(cities);
        }
    }
}