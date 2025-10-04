namespace xtramile_server
{
    public class AppSettings
    {
        public OpenWeatherSettings OpenWeather { get; set; } = new();
    }

    public class OpenWeatherSettings
    {
        public string BaseUrl { get; set; } = "https://api.openweathermap.org";
        public string ApiKey { get; set; } = "";
    }
}