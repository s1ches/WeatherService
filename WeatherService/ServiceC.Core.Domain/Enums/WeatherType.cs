using System.ComponentModel;

namespace ServiceC.Core.Domain.Enums;

public enum WeatherType
{
    [Description("Sunny")]
    Sunny = 1,
    
    [Description("Mostly Sunny")]
    MostlySunny = 2,
    
    [Description("Partly Sunny")]
    PartlySunny = 3,
    
    [Description("Intermittent Clouds Day")]
    IntermittentCloudsDay = 4,
    
    [Description("Hazy Sunshine")]
    HazySunshine = 5,
    
    [Description("Mostly Cloudy Day")]
    MostlyCloudyDay = 6,
    
    [Description("Cloudy")]
    Cloudy = 7,
    
    [Description("Dreary (Overcast)")]
    Dreary = 8,
    
    [Description("Fog")]
    Fog = 11,
    
    [Description("Showers")]
    Showers = 12,
    
    [Description("Mostly Cloudy Showers Day")]
    MostlyCloudyShowersDay = 13,
    
    [Description("Partly Sunny Showers")]
    PartlySunnyShowers = 14,
    
    [Description("T-Storms")]
    TStorms = 15,
    
    [Description("Mostly Cloudy T-Storms Day")]
    MostlyCloudyTStormsDay = 16,
    
    [Description("Partly Sunny T-Storms")]
    PartlySunnyTStorms = 17,
    
    [Description("Rain")]
    Rain = 18,
    
    [Description("Flurries")]
    Flurries = 19,
    
    [Description("Mostly Cloudy Flurries Day")]
    MostlyCloudyFlurriesDay = 20,
    
    [Description("Partly Sunny Flurries")]
    PartlySunnyFlurries = 21,
    
    [Description("Snow")]
    Snow = 22,
    
    [Description("Mostly Cloudy Snow Day")]
    MostlyCloudySnowDay = 23,
    
    [Description("Ice")]
    Ice = 24,
    
    [Description("Sleet")]
    Sleet = 25,
    
    [Description("Freezing Rain")]
    FreezingRain = 26,
    
    [Description("Rain and Snow")]
    RainAndSnow = 29,
    
    [Description("Hot")]
    Hot = 30,
    
    [Description("Cold")]
    Cold = 31,
    
    [Description("Windy")]
    Windy = 32,
    
    [Description("Clear")]
    Clear = 33,
    
    [Description("Mostly Clear")]
    MostlyClear = 34,
    
    [Description("Partly Cloudy")]
    PartlyCloudy = 35,
    
    [Description("Intermittent Clouds Night")]
    IntermittentCloudsNight = 36,
    
    [Description("Hazy Moonlight")]
    HazyMoonlight = 37,
    
    [Description("Mostly Cloudy Night")]
    MostlyCloudyNight = 38,
    
    [Description("PartlyCloudyShowers")]
    PartlyCloudyShowers = 39,
    
    [Description("Mostly Cloudy Showers Night")]
    MostlyCloudyShowersNight = 40,
    
    [Description("Partly Cloudy T-Storms")]
    PartlyCloudyTStorms = 41,
    
    [Description("Mostly Cloudy T-Storms Night")]
    MostlyCloudyTStormsNight = 42,
    
    [Description("Mostly Cloudy Flurries Night")]
    MostlyCloudyFlurriesNight = 43,
    
    [Description("Mostly Cloudy Snow Night")]
    MostlyCloudySnowNight = 44
}