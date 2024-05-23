using ServiceB.WorkerWeatherConsumer.Interfaces;
using ServiceB.WorkerWeatherConsumer.Services;

namespace ServiceB.WorkerWeatherConsumer.Configuration;

public static class AddServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddLogging();

        services.AddSingleton<IWeatherInteractionService, WeatherInteractionService>();
        
        return services;
    }
}