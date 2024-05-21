using Microsoft.Extensions.DependencyInjection;

namespace ServiceC.Core.Application;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        => services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(typeof(IServiceCollectionExtension).Assembly));
}