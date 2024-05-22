using System.Text;
using System.Text.Json;
using Confluent.Kafka;

namespace ServiceA.WorkerWeatherCollector.Serializers;

public class BaseSerializer<T> : ISerializer<T>
{
    public byte[] Serialize(T data, SerializationContext context)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions(JsonSerializerOptions.Default)
        {
            IncludeFields = true
        });
        
        return Encoding.UTF8.GetBytes(json);
    }
}