using System.Runtime.Serialization;
using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using ServiceA.WorkerWeatherCollector.Interfaces;
using ServiceA.WorkerWeatherCollector.Models;
using ServiceC;

namespace ServiceA.WorkerWeatherCollector.Services;

public class WeatherCollector(IConfiguration configuration, HttpClient client) : IWeatherCollector
{
    private readonly string _apiKey = configuration["WeatherApiConfig:WeatherApiKey"]!;

    private readonly string _weatherResourceUrl = configuration["WeatherApiConfig:WeatherResourceUrl"]!;
    
    public async Task<SetWeatherRequest?> CollectWeatherAsync(int cityKey)
    {
        var requestUri = $"{_weatherResourceUrl}{cityKey}?apikey={_apiKey}&language=ru-RU&details=false";
        var response = await client.GetStringAsync(requestUri);

        var weather = JsonSerializer.Deserialize<List<WeatherCollectionResult>>(response)?.FirstOrDefault();

        if (weather == null)
            throw new SerializationException("Cannot deserialize data");
        
        var doc = JsonDocument.Parse(response).RootElement;

        weather.TemperatureInCelsius = doc[0].GetProperty("Temperature")
                .GetProperty("Metric")
                .GetProperty("Value")
                .GetDouble();
        
        return new SetWeatherRequest
        {
            IsDayTime = weather.IsDayTime,
            LocalObservationDateTime = weather.LocalObservationDateTime
                .ToUniversalTime()
                .ToTimestamp(),
            HasPrecipitation = weather.HasPrecipitation,
            PrecipitationType = weather.PrecipitationType,
            TemperatureInCelsius = weather.TemperatureInCelsius,
            WeatherIcon = weather.WeatherIcon,
            WeatherText = weather.WeatherText            
        };
    }
}