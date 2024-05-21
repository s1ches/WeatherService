using ServiceA.WorkerWeatherCollector.Interfaces;
using ServiceA.WorkerWeatherCollector.Services;

namespace ServiceA.WorkerWeatherCollector.Configuration;

public static class AddServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddLogging();
        return services.AddScoped<IWeatherCollector, WeatherCollector>();
    }
}