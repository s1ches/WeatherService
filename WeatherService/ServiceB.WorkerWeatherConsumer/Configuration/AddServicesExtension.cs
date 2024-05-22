using Confluent.Kafka;
using ServiceB.WorkerWeatherConsumer.Deserializers;

namespace ServiceB.WorkerWeatherConsumer.Configuration;

public static class AddServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddLogging();

        services.AddScoped(typeof(IDeserializer<>), typeof(BaseDeserializer<>));

        return services;
    }
}