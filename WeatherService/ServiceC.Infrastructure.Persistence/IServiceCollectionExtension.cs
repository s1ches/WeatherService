using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceC.Core.Application.Interfaces;

namespace ServiceC.Infrastructure.Persistence;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddPersistenceLayer(
        this IServiceCollection services,
        IConfiguration configuration)
        => services.AddDbContext<IWeatherDbContext, WeatherDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
}