# Design Notes
- Units: Call OpenWeather with imperial units (°F). Service converts to metric (°C) and returns both.
- Fallback: If the HTTP call fails or null or key is empty, return a fixed mock.
- DI: IWeatherService wraps HTTP; ICountryStore is in-memory; ITemperatureConverter is stateless.
- Tests:
  - Converter: known points + rounding
  - WeatherService: success path + all fallbacks
  - Controllers: OK / NotFound with fakes