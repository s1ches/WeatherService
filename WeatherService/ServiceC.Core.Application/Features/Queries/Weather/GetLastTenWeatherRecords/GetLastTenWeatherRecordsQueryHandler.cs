using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceC.Core.Application.Interfaces;
using ServiceC.Shared.Contracts.Requests.Weather;

namespace ServiceC.Core.Application.Features.Queries.Weather.GetLastTenWeatherRecords;

public class GetLastTenWeatherRecordsQueryHandler(IWeatherDbContext dbContext)
    : IRequestHandler<GetLastTenWeatherRecordsQuery, GetLastTenWeatherRecordsResponse>
{
    public async Task<GetLastTenWeatherRecordsResponse> Handle(
        GetLastTenWeatherRecordsQuery request
        , CancellationToken cancellationToken)
    {
        var records = dbContext.WeatherRecords
            .OrderByDescending(record => record.CreateDate)
            .Select(record => new GetLastTenWeatherRecordsResponseItem
            {
                LocalObservationDateTime = record.LocalObservationDateTime,
                WeatherText = record.WeatherText,
                WeatherType = record.WeatherType.ToString(),
                IsDayTime = record.IsDayTime,
                PrecipitationType = record.PrecipitationType.ToString(),
                TemperatureInCelsius = record.TemperatureInCelsius
            });

        return new GetLastTenWeatherRecordsResponse
        {
            TotalCount = await records.CountAsync(cancellationToken),
            Records = await records.Take(10).ToListAsync(cancellationToken)
        };
    }
}