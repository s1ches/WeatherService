using Confluent.Kafka;
using ServiceB.WorkerWeatherConsumer.Deserializers;
using ServiceB.WorkerWeatherConsumer.Interfaces;
using ServiceB.WorkerWeatherConsumer.Services;

namespace ServiceB.WorkerWeatherConsumer.Configuration;

public static class AddServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddLogging();

        services.AddSingleton(typeof(IDeserializer<>), typeof(BaseDeserializer<>));
        services.AddSingleton<IWeatherInteractionService, WeatherInteractionService>();
        
        return services;
    }
}