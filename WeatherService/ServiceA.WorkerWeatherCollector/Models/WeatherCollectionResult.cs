using System.Text.Json.Serialization;

namespace ServiceA.WorkerWeatherCollector.Models;

public class WeatherCollectionResult
{
    [JsonPropertyName("LocalObservationDateTime")]
    public DateTime LocalObservationDateTime { get; set; }

    [JsonPropertyName("WeatherText")] 
    public string WeatherText { get; set; } = string.Empty;
    
    [JsonPropertyName("WeatherIcon")]
    public int? WeatherIcon { get; set; }
    
    [JsonPropertyName("HasPrecipitation")]
    public bool HasPrecipitation { get; set; }
    
    [JsonPropertyName("PrecipitationType")]
    public string? PrecipitationType { get; set; }
    
    [JsonPropertyName("IsDayTime")]
    public bool IsDayTime { get; set; }

    public double? TemperatureInCelsius;
}