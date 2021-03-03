using FullStack.API.Services.Interfaces;
using FullStack.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FullStack.API.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ICacheService _cacheService;
        private readonly ILogger<WeatherService> _logger;

        private const string CACHE_KEY = "WeatherCache";

        public WeatherService(HttpClient httpClient, ICacheService cacheService, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<WeatherData> GetWeatherDataAsync()
        {
            var cacheData = await _cacheService.GetValueAsync(CACHE_KEY);
            if (cacheData != null)
            {
                _logger.LogInformation("Getting weather data from cache...");
                var weatherCacheData = JsonConvert.DeserializeObject<WeatherData>(cacheData);        
                return weatherCacheData;
            }

            var city = Environment.GetEnvironmentVariable("WeatherCity");
            var apiKey = Environment.GetEnvironmentVariable("WeatherApiKey");

            var apiUri = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=es";
            var responseString = await _httpClient.GetStringAsync(apiUri);

            JToken jsonObj = JObject.Parse(responseString);

            var data = new WeatherData
            {
                Description = (string) jsonObj["weather"][0]["description"],
                FeelsLike = (double)jsonObj["main"]["feels_like"],
                Humidity = (double)jsonObj["main"]["humidity"],
                MaxTemperature = (double)jsonObj["main"]["temp_max"],
                MinTemperature = (double)jsonObj["main"]["temp_min"],
                Pressure = (double)jsonObj["main"]["pressure"],
                Sunrise = (int)jsonObj["sys"]["sunrise"],
                Sunset = (int)jsonObj["sys"]["sunset"],
                Temperature = (double)jsonObj["main"]["temp"],
                Icon = (string)jsonObj["weather"][0]["icon"],
                Wind = new Wind
                {
                    Degrees = (int)jsonObj["wind"]["deg"],
                    Speed = (double)jsonObj["wind"]["speed"]
                }
            };

            await _cacheService.SetValueAsync(CACHE_KEY, JsonConvert.SerializeObject(data));
            return data;
        }
    }
}
