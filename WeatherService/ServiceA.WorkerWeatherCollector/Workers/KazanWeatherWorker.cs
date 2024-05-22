using ServiceA.WorkerWeatherCollector.Interfaces;
using ServiceA.WorkerWeatherCollector.Models;
using ServiceA.WorkerWeatherCollector.Serializers;

namespace ServiceA.WorkerWeatherCollector.Workers;

public class KazanWeatherWorker(
    IWeatherCollector weatherCollector,
    IProducer<WeatherCollectionResult, BaseSerializer<WeatherCollectionResult>> producer,
    IConfiguration configuration,
    ILogger<IWeatherCollector> logger) : IWorker
{
    private readonly int _cityKey = int.Parse(configuration["WeatherApiConfig:CityKey"]!);

    public async Task RunAsync()
    {
        var weather = await weatherCollector.CollectWeatherAsync(_cityKey);
        
        if (weather is not null)
        {
            logger.LogInformation($"Collected weather at {DateTime.UtcNow}");
            await producer.ProduceMessageAsync(weather, "weather");
        }
        else logger.LogError($"Data wasn't collected at {DateTime.UtcNow}");
    }
}