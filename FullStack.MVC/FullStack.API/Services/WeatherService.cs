using FullStack.API.Services.Interfaces;
using FullStack.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FullStack.API.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherData> GetWeatherDataAsync()
        {
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

            return data;
        }
    }
}
