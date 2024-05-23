using Hangfire;
using Hangfire.PostgreSql;
using ServiceA.WorkerWeatherCollector.Workers;

namespace ServiceA.WorkerWeatherCollector.Configuration;

public static class AddWorkersExtension
{
    public static IServiceCollection AddWorkers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(h =>
            h.UsePostgreSqlStorage(configuration.GetConnectionString("DefaultConnection")));

        return services.AddHangfireServer();
    }
}