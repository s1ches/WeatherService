using ServiceA.WorkerWeatherCollector.Models;

namespace ServiceA.WorkerWeatherCollector.Interfaces;

public interface IWeatherCollector
{ 
    Task<WeatherCollectionResult?> CollectWeatherAsync(int cityKey);
}