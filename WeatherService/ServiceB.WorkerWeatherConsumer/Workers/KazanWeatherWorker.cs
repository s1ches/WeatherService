using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry.Serdes;
using ServiceB.WorkerWeatherConsumer.Interfaces;
using ServiceC;

namespace ServiceB.WorkerWeatherConsumer.Workers;

public class KazanWeatherWorker(
    IConfiguration configuration,
    IWeatherInteractionService weatherInteractionService,
    ILogger<KazanWeatherWorker> logger)
    : BackgroundService
{
    private readonly ConsumerConfig _config = new()
    {
        BootstrapServers = configuration["Kafka:BootstrapServers"],
        AutoOffsetReset = AutoOffsetReset.Earliest,
        GroupId = "WeatherGroup"
    };

    private const string TopicName = "weather";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var consumer = new ConsumerBuilder<Null, SetWeatherRequest>(_config)
            .SetValueDeserializer(new ProtobufDeserializer<SetWeatherRequest>().AsSyncOverAsync())
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
                    $" {result.Message.Value.TemperatureInCelsius} at {DateTime.UtcNow}");

                await weatherInteractionService.SetWeather(result.Message.Value, stoppingToken);
            }
            catch (ConsumeException exception)
            {
                logger.LogError($"Consume error: {exception.Message} at {DateTime.UtcNow}");
            }
        }
    }
}