using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ServiceC.Core.Domain.Entities;

namespace ServiceC.Core.Application.Interfaces;

public interface IWeatherDbContext
{
    DbSet<WeatherRecord> WeatherRecords { get; set; }
    
    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}