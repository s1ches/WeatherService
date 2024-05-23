using ServiceC;

namespace ServiceB.WorkerWeatherConsumer.Interfaces;

public interface IWeatherInteractionService
{
    Task SetWeather(SetWeatherRequest weather, CancellationToken cancellationToken);
}