using System.ComponentModel;

namespace ServiceC.Core.Domain.Enums;

public enum PrecipitationType
{
    [Description("Rain")]
    Rain = 1,
    
    [Description("Snow")]
    Snow = 2,
    
    [Description("Ice")]
    Ice = 3,
    
    [Description("Mixed")]
    Mixed = 4
}