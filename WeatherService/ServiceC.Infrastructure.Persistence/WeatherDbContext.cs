using Microsoft.EntityFrameworkCore;
using ServiceC.Core.Application.Interfaces;
using ServiceC.Core.Domain.Entities;
using ServiceC.Infrastructure.Persistence.EntitiesConfigurations;

namespace ServiceC.Infrastructure.Persistence;

public class WeatherDbContext : DbContext, IWeatherDbContext
{
    public WeatherDbContext()
    {
    }

    public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WeatherRecordConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<WeatherRecord> WeatherRecords { get; set; }
}