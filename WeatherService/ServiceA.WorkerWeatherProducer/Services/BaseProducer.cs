using Common.WeatherCommon.Models;
using Confluent.Kafka;

namespace ServiceA.WorkerWeatherCollector.Services;

public class BaseProducer<TMessage, TMessageSerializer>(
    IConfiguration configuration,
    ILogger<BaseProducer<TMessage, TMessageSerializer>> logger)
    : ServiceA.WorkerWeatherCollector.Interfaces.IProducer<TMessage, TMessageSerializer>
    where TMessageSerializer : ISerializer<TMessage>, new()
{
    private readonly ProducerConfig _producerConfig = new()
    {
        BootstrapServers = configuration["Kafka:BootstrapServers"]
    };
    
    public async Task ProduceMessageAsync(TMessage message, string topicName, CancellationToken cancellationToken)
    {
        try
        {
            using var producer = new ProducerBuilder<Null, TMessage>(_producerConfig)
                .SetValueSerializer(new TMessageSerializer())
                .Build();

            var result = await producer.ProduceAsync(topicName, new Message<Null, TMessage>
            {
                Value = message
            }, cancellationToken);

            if (result.Status == PersistenceStatus.NotPersisted)
                logger.LogWarning($"Message wasn't persisted at {DateTime.UtcNow}");

            logger.LogInformation($"Message was persisted at {DateTime.UtcNow}");

            producer.Flush(cancellationToken);
        }
        catch (ProduceException<Null, WeatherCollectionResult> exception)
        {
            logger.LogError($"{exception.Message} at {DateTime.UtcNow}");
        }
    }
}