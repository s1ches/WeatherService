﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ServiceC.Infrastructure.Persistence;

#nullable disable

namespace ServiceC.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(WeatherDbContext))]
    partial class WeatherDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ServiceC.Core.Domain.Entities.WeatherRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDayTime")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LocalObservationDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PrecipitationType")
                        .HasColumnType("text");

                    b.Property<double?>("TemperatureInCelsius")
                        .HasColumnType("double precision");

                    b.Property<string>("WeatherText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WeatherType")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreateDate");

                    b.ToTable("WeatherRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
