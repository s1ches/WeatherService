using Common.WeatherCommon.Models;
using Confluent.Kafka;
using Google.Protobuf.WellKnownTypes;
using ServiceB.WorkerWeatherConsumer.Deserializers;
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
                    $" {result.Message.Value.TemperatureInCelsius} at {DateTime.UtcNow}");

                var request = new SetWeatherRequest
                {
                    IsDayTime = result.Message.Value.IsDayTime,
                    LocalObservationDateTime = result.Message.Value.LocalObservationDateTime
                        .ToUniversalTime()
                        .ToTimestamp(),
                    HasPrecipitation = result.Message.Value.HasPrecipitation,
                    PrecipitationType = result.Message.Value.PrecipitationType,
                    TemperatureInCelsius = result.Message.Value.TemperatureInCelsius,
                    WeatherIcon = result.Message.Value.WeatherIcon,
                    WeatherText = result.Message.Value.WeatherText
                };

                await weatherInteractionService.SetWeather(request, stoppingToken);
            }
            catch (ConsumeException exception)
            {
                logger.LogError($"Consume error: {exception.Message} at {DateTime.UtcNow}");
            }
        }
    }
}