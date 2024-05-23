using Grpc.Core;
using Grpc.Net.Client;
using ServiceB.WorkerWeatherConsumer.Interfaces;
using ServiceC;

namespace ServiceB.WorkerWeatherConsumer.Services;

public class WeatherInteractionService(IConfiguration configuration, ILogger<WeatherInteractionService> logger)
    : IWeatherInteractionService
{
    public async Task SetWeather(SetWeatherRequest request, CancellationToken cancellationToken)
    { 
        using var channel = GrpcChannel.ForAddress(configuration["GrpcConfiguration:ServiceCAddress"]!);
        var client = new WeatherInteraction.WeatherInteractionClient(channel);
        
        try
        {
            await client.SetWeatherAsync(request, cancellationToken: cancellationToken);
            logger.LogInformation($"Message was sent at {DateTime.UtcNow}");
        }
        catch (RpcException exception)
        {
            logger.LogInformation($"{exception.Message} at {DateTime.UtcNow}");
        }
    }
}