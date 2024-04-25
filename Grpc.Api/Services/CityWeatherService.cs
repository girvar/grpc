using Grpc.Contracts;
using Grpc.Core;
using Microsoft.Extensions.Logging;
namespace Grpc.Api
{
    public class CityWeatherService : WeatherService.WeatherServiceBase
    {
        private readonly ILogger<CityWeatherService> _logger;

        public CityWeatherService(ILogger<CityWeatherService> logger)
        {
            _logger = logger;
        }

        public override Task<CityWeatherResponse> GetWeather(CityWeatherRequest request, ServerCallContext context)
        {
            CityWeatherResponse weatherResponse = GetWeather(request.City);
            return Task.FromResult(weatherResponse);
        }

        private CityWeatherResponse GetWeather(string city)
        {
            CityWeatherResponse weatherResponse = new CityWeatherResponse()
            {
                City = city,
                WeatherData = new WeatherData()
                {
                    Forecast = "Cloudy",
                    High = "100.0",
                    Low = "90.0"
                }
            };
            return weatherResponse;
        }
    }
}