using Microsoft.Extensions.DependencyInjection;
using ServiceC.Core.Application.Interfaces;
using ServiceC.Infrastructure.Services.Services;

namespace ServiceC.Infrastructure.Services;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
        => services.AddScoped<IGRPCWeatherInteractionService, GRPCWeatherInteractionService>();
}