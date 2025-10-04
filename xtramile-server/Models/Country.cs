namespace xtramile_server.Models
{
    public class Country
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";

        public Country() { }

        public Country(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}