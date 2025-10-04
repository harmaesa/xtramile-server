using Newtonsoft.Json;

namespace xtramile_server.Models
{
    public class OpenWeatherResponse
    {
        [JsonProperty("sys")]
        public Sys Sys { get; set; } = new();

        [JsonProperty("main")]
        public Main Main { get; set; } = new();

        [JsonProperty("wind")]
        public Wind Wind { get; set; } = new();

        [JsonProperty("weather")]
        public WeatherCond[] Weather { get; set; } = [];

        [JsonProperty("visibility")]
        public int Visibility { get; set; }
    }

    public class Sys
    {
        [JsonProperty("country")]
        public string? Country { get; set; }
    }

    public class Main
    {
        [JsonProperty("temp")]
        public double? Temp { get; set; }

        [JsonProperty("humidity")]
        public int? Humidity { get; set; }

        [JsonProperty("pressure")]
        public int? Pressure { get; set; }

        [JsonProperty("dew_point")]
        public double? DewPoint { get; set; }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public double? Speed { get; set; }

        [JsonProperty("deg")]
        public int? Deg { get; set; }
    }

    public class WeatherCond
    {
        [JsonProperty("main")]
        public string? Main { get; set; }
    }
}