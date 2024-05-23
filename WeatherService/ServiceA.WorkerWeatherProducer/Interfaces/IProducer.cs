using Confluent.Kafka;

namespace ServiceA.WorkerWeatherCollector.Interfaces;

public interface IProducer<in TMessage, in TMessageSerializer> 
    where TMessageSerializer: ISerializer<TMessage>, new()
{
    Task ProduceMessageAsync(TMessage message, string topicName, CancellationToken cancellationToken = default);
}