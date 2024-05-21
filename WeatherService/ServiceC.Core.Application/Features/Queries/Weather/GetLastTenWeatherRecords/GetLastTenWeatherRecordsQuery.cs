using MediatR;
using ServiceC.Shared.Contracts.Requests.Weather;

namespace ServiceC.Core.Application.Features.Queries.Weather.GetLastTenWeatherRecords;

public record GetLastTenWeatherRecordsQuery: IRequest<GetLastTenWeatherRecordsResponse>;