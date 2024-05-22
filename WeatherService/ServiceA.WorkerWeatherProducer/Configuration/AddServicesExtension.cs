using Confluent.Kafka;
using ServiceA.WorkerWeatherCollector.Interfaces;
using ServiceA.WorkerWeatherCollector.Serializers;
using ServiceA.WorkerWeatherCollector.Services;

namespace ServiceA.WorkerWeatherCollector.Configuration;

public static class AddServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddLogging();

        services.AddScoped(typeof(Interfaces.IProducer<,>), typeof(BaseProducer<,>));
        services.AddScoped(typeof(ISerializer<>), typeof(BaseSerializer<>));
        return services.AddScoped<IWeatherCollector, WeatherCollector>();
    }
}