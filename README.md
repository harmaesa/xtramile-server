# Xtramile Server (ASP.NET Core, .NET 8)
Small Web API:
- Countries & cities (in-memory)
- Current weather from OpenWeather
- If the API call fails or key missing, returns a mock

# Prereqs
- .NET 8 SDK

# Configure
Set your OpenWeather key. For local, included in appsettings already.

# Run
```
cd xtramile-server
dotnet dev-certs https --trust
dotnet run --launch-profile https
```

# Endpoints
Open API or Swagger (Development): https://localhost:7297/swagger

# Example
curl "https://localhost:7297/api/weather/Bandung"

# Tests
dotnet test