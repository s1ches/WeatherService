using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Google.Protobuf;
using ProtoBuf.Meta;

namespace ServiceA.WorkerWeatherCollector.Services;

public class BaseProtobufProducer<TMessage>(
    IConfiguration configuration,
    ILogger<BaseProtobufProducer<TMessage>> logger)
    : Interfaces.IProducer<TMessage> where TMessage : IMessage<TMessage>, new()
{
    private readonly ProducerConfig _producerConfig = new()
    {
        BootstrapServers = configuration["Kafka:BootstrapServers"]
    };
    
    private readonly SchemaRegistryConfig _schemaRegistryConfig = new()
    {
        Url = configuration["Kafka:SchemaRegistryConfigUrl"]
    };
    
    public async Task ProduceMessageAsync(TMessage message, string topicName, CancellationToken cancellationToken)
    {
        try
        {
            using var schemaRegistryClient = new CachedSchemaRegistryClient(_schemaRegistryConfig);

            await RegisterSchemaAsync(schemaRegistryClient);
            
            using var producer = new ProducerBuilder<Null, TMessage>(_producerConfig)
                .SetValueSerializer(new ProtobufSerializer<TMessage>(schemaRegistryClient))
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
        catch (ProduceException<Null, TMessage> exception)
        {
            logger.LogError($"{exception.Message} at {DateTime.UtcNow}");
        }
    }

    private static async Task RegisterSchemaAsync(ISchemaRegistryClient client)
    {
        var schema = RuntimeTypeModel.Default.GetSchema(typeof(TMessage));

        var subjects = await client.GetAllSubjectsAsync();
        
        if(!subjects.Contains(typeof(TMessage).Name))
            await client
            .RegisterSchemaAsync(typeof(TMessage).Name, new Schema(schema, SchemaType.Protobuf));
    }
}