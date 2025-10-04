namespace xtramile_server.Models
{
    public class City
    {
        public string Name { get; set; } = "";
        public string CountryCode { get; set; } = "";

        public City() { }

        public City(string name, string countryCode)
        {
            Name = name;
            CountryCode = countryCode;
        }
    }
}