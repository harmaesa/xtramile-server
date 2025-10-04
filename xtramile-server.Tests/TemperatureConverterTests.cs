using xtramile_server.Services;

namespace xtramile_server.Tests
{
    public class TemperatureConverterTests
    {
        [Theory]
        [InlineData(32, 0)]
        [InlineData(212, 100)]
        [InlineData(-40, -40)]
        [InlineData(77, 25)]
        [InlineData(41, 5)]
        [InlineData(98.6, 37)]
        public void ConvertFahrenheitToCelsiusReturnsExpected(double fahrenheit, double expectedCelsius)
        {
            var converter = new TemperatureConverter();
            var result = converter.FahrenheitToCelsius(fahrenheit);
            Assert.Equal(expectedCelsius, result);
        }

        [Theory]
        [InlineData(33.809, 1.01)]
        [InlineData(30.191, -1.01)]
        public void ConvertFahrenheitToCelsiusRoundsAwayFromZeroAtMidpoint(double fahrenheit, double expectedCelsius)
        {
            var converter = new TemperatureConverter();
            var result = converter.FahrenheitToCelsius(fahrenheit);
            Assert.Equal(expectedCelsius, result, 2);
        }
    }
}