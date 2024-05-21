namespace ServiceC.Shared.Contracts.Requests.Weather;

public class GetLastTenWeatherRecordsResponseItem
{
    public DateTime LocalObservationDateTime { get; set; }
    
    public string WeatherText { get; set; } = string.Empty;
    
    public string? WeatherType { get; set; }
    
    public bool IsDayTime { get; set; }

    public string? PrecipitationType { get; set; }
    
    public double? TemperatureInCelsius { get; set; }
}