namespace xtramile_server.Services
{
    public interface ITemperatureConverter
    {
        double FahrenheitToCelsius(double fahrenheit);
    }

    public class TemperatureConverter : ITemperatureConverter
    {
        public double FahrenheitToCelsius(double fahrenheit) => (double)Math.Round(((decimal)fahrenheit - 32m) * 5m / 9m, 2, MidpointRounding.AwayFromZero);
    }
}