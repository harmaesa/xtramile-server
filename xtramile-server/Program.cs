using xtramile_server.Services;

namespace xtramile_server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<AppSettings>(builder.Configuration);
            builder.Services.AddSingleton<ICountryStore, CountryStore>();
            builder.Services.AddSingleton<ITemperatureConverter, TemperatureConverter>();
            builder.Services.AddHttpClient<IWeatherService, WeatherService>();

            var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod());
            });

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCors();
            app.MapControllers();
            app.Run();
        }
    }
}