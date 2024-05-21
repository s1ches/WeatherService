using ServiceC.Core.Domain.BaseEntities;
using ServiceC.Core.Domain.Enums;

namespace ServiceC.Core.Domain.Entities;

public class WeatherRecord : BaseAuditableEntity
{
    public DateTime LocalObservationDateTime { get; set; }
    
    public string WeatherText { get; set; } = string.Empty;
    
    public WeatherType? WeatherType { get; set; }
    
    public bool IsDayTime { get; set; }

    public PrecipitationType? PrecipitationType { get; set; }
    
    public double? TemperatureInCelsius { get; set; }
}