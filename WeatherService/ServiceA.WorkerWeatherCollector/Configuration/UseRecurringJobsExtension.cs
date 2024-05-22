using Hangfire;
using ServiceA.WorkerWeatherCollector.Workers;

namespace ServiceA.WorkerWeatherCollector.Configuration;

public static class UseRecurringJobsExtension
{
    public static WebApplication UseRecurringJobs(this WebApplication app, IConfiguration configuration)
    {
        RecurringJob.AddOrUpdate<KazanWeatherWorker>(
            typeof(KazanWeatherWorker).FullName,
            x => x.RunAsync(),
            configuration["WorkersConfiguration:KazanWeatherWorker:Cron"],
            TimeZoneInfo.Utc);

        return app;
    }
}