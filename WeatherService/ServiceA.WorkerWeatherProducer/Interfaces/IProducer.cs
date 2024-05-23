using Google.Protobuf;

namespace ServiceA.WorkerWeatherCollector.Interfaces;

public interface IProducer<in TMessage> where TMessage : IMessage<TMessage>, new()
{
    Task ProduceMessageAsync(TMessage message, string topicName, CancellationToken cancellationToken = default);
}