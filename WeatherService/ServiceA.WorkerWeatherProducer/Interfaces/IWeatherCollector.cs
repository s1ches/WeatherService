using Common.WeatherCommon.Models;

namespace ServiceA.WorkerWeatherCollector.Interfaces;

public interface IWeatherCollector
{ 
    Task<WeatherCollectionResult?> CollectWeatherAsync(int cityKey);
}