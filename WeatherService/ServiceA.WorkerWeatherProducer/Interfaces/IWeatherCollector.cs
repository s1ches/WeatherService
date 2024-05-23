using ServiceC;

namespace ServiceA.WorkerWeatherCollector.Interfaces;

public interface IWeatherCollector
{ 
    Task<SetWeatherRequest?> CollectWeatherAsync(int cityKey);
}