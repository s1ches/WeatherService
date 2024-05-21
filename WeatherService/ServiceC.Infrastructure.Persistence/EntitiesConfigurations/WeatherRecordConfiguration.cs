using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceC.Core.Domain.Entities;
using ServiceC.Core.Domain.Enums;

namespace ServiceC.Infrastructure.Persistence.EntitiesConfigurations;

public class WeatherRecordConfiguration : IEntityTypeConfiguration<WeatherRecord>
{
    public void Configure(EntityTypeBuilder<WeatherRecord> builder)
    {
        builder.HasKey(record => record.Id);

        builder.HasIndex(record => record.CreateDate);

        builder.Property(record => record.WeatherType)
            .HasConversion(e => e.ToString() ?? null,
                s => s == null
                    ? null
                    : (WeatherType)Enum.Parse(typeof(WeatherType), s));

        builder.Property(record => record.PrecipitationType)
            .HasConversion(e => e.ToString() ?? null,
                s => s == null
                    ? null
                    : (PrecipitationType)Enum.Parse(typeof(PrecipitationType), s));
    }
}