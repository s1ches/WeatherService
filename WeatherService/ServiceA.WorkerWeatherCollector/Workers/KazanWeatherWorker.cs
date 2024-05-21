using ServiceA.WorkerWeatherCollector.Interfaces;

namespace ServiceA.WorkerWeatherCollector.Workers;

public class KazanWeatherWorker(
    IWeatherCollector weatherCollector,
    IConfiguration configuration,
    ILogger<IWeatherCollector> logger) : IWorker
{
    private readonly int _cityKey = int.Parse(configuration["WeatherApiConfig:CityKey"]!);

    public async Task RunAsync()
    {
        var weather = await weatherCollector.CollectWeatherAsync(_cityKey);
        logger.LogInformation($"Collected weather at {DateTime.UtcNow}");
    }
}