namespace xtramile_server.Models
{
    public class Weather
    {
        public string City { get; set; } = "";
        public string Country { get; set; } = "";
        public DateTime UtcTime { get; set; }
        public double WindSpeed { get; set; }
        public int WindDirection { get; set; }
        public int Visibility { get; set; }
        public string Sky { get; set; } = "";
        public double TemperatureF { get; set; }
        public double TemperatureC { get; set; }
        public double DewPoint { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }

        public Weather() { }

        public Weather(
            string city,
            string country,
            DateTime utcTime,
            double windSpeed,
            int windDirection,
            int visibility,
            string sky,
            double temperatureF,
            double temperatureC,
            double dewPoint,
            int humidity,
            int pressure)
        {
            City = city;
            Country = country;
            UtcTime = utcTime;
            WindSpeed = windSpeed;
            WindDirection = windDirection;
            Visibility = visibility;
            Sky = sky;
            TemperatureF = temperatureF;
            TemperatureC = temperatureC;
            DewPoint = dewPoint;
            Humidity = humidity;
            Pressure = pressure;
        }
    }
}