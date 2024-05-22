using Common.WeatherCommon.Models;
using Confluent.Kafka;
using ServiceB.WorkerWeatherConsumer.Deserializers;

namespace ServiceB.WorkerWeatherConsumer.Workers;

public class KazanWeatherWorker(IConfiguration configuration, ILogger<KazanWeatherWorker> logger)
    : BackgroundService
{
    private readonly ConsumerConfig _config = new()
    {
        BootstrapServers = configuration["Kafka:BootstrapServers"],
        AutoOffsetReset = AutoOffsetReset.Earliest,
        GroupId = "WeatherGroup"
    };

    private const string TopicName = "weather";

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var consumer = new ConsumerBuilder<Null, WeatherCollectionResult>(_config)
            .SetValueDeserializer(new BaseDeserializer<WeatherCollectionResult>())
            .Build();

        consumer.Subscribe(TopicName);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);

                if (result is null)
                    continue;

                if (result.IsPartitionEOF)
                {
                    logger.LogWarning($"End of partition at {DateTime.UtcNow}");
                    continue;
                }

                logger.LogInformation(
                    $"Successful consume result:" +
                    $" {result.Message.Value.LocalObservationDateTime} at {DateTime.UtcNow}");
            }
            catch (ConsumeException exception)
            {
                logger.LogError($"Consume error: {exception.Message} at {DateTime.UtcNow}");
            }
        }

        return Task.CompletedTask;
    }
}