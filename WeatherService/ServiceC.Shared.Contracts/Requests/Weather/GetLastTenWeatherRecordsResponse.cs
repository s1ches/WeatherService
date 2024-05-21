namespace ServiceC.Shared.Contracts.Requests.Weather;

public class GetLastTenWeatherRecordsResponse
{
    public List<GetLastTenWeatherRecordsResponseItem> Records { get; set; } = [];
    
    public int TotalCount { get; set; }
}