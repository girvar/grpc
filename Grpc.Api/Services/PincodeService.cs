﻿using Grpc.Contracts;
using Grpc.Core;
using Microsoft.Extensions.Logging;
namespace Grpc.Api
{
    public class PincodeService : Pincode.PincodeBase
    {
        private readonly ILogger<PincodeService> _logger;
        private readonly IDictionary<string, string> _pincodeDictionary = new Dictionary<string, string>
        {
            { "Pune", "411001" },
            { "Mumbai", "400001" },
            { "Delhi", "110001" }
        };
        public PincodeService(ILogger<PincodeService> logger)
        {
            _logger = logger;
        }

        public override Task<PincodeResponse> GetPincode(PincodeRequest request, ServerCallContext context)
        {
            WeatherResponse weatherResponse = GetWeather(request.City);

            if (_pincodeDictionary.ContainsKey(request.City))
            {
                var pincode = _pincodeDictionary[request.City];
                return Task.FromResult(new PincodeResponse
                {
                    City = request.City,
                    Pincode = pincode,
                    WeatherResponse = weatherResponse
                });
            }
            else
            {
                return Task.FromResult(new PincodeResponse
                {
                    City = request.City,
                    Pincode = "NotFound",
                    WeatherResponse = weatherResponse
                });
            }
        }

        private WeatherResponse GetWeather(string city)
        {
            WeatherResponse weatherResponse = new WeatherResponse()
            {
                City = city,
                WeatherReport = new WeatherReport()
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