using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ServiceC.Core.Application.Interfaces;
using ServiceC.Core.Domain.Entities;
using ServiceC.Core.Domain.Enums;
using Enum = System.Enum;

namespace ServiceC.Infrastructure.Grpc.Services;

public class WeatherInteractionService(IWeatherDbContext dbContext) : WeatherInteraction.WeatherInteractionBase
{
    public override async Task<Empty> SetWeather(SetWeatherRequest request, ServerCallContext context)
    {
        var weatherRecord = new WeatherRecord
        {
            CreateDate = DateTime.UtcNow,
            EditDate = DateTime.UtcNow,
            IsDayTime = request.IsDayTime,
            LocalObservationDateTime = request.LocalObservationDateTime.ToDateTime().ToUniversalTime(),
            PrecipitationType = (PrecipitationType)Enum.Parse(typeof(PrecipitationType), request.PrecipitationType),
            TemperatureInCelsius = request.TemperatureInCelsius,
            WeatherText = request.WeatherText,
            WeatherType = request.WeatherIcon is null
                ? null
                : (WeatherType)request.WeatherIcon
        };

        await dbContext.WeatherRecords.AddAsync(weatherRecord, context.CancellationToken);
        await dbContext.SaveChangesAsync(context.CancellationToken);

        return new Empty();
    }
}