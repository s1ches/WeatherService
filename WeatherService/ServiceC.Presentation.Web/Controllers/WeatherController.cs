using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceC.Core.Application.Features.Queries.Weather.GetLastTenWeatherRecords;
using ServiceC.Shared.Contracts.Requests.Weather;

namespace ServiceC.Presentation.Web.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class WeatherController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetLastTenWeatherRecords")]
    public async Task<GetLastTenWeatherRecordsResponse> GetLastTenWeatherRecords(CancellationToken cancellationToken) 
        => await mediator.Send(new GetLastTenWeatherRecordsQuery(), cancellationToken);
    
}