using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceC.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LocalObservationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WeatherText = table.Column<string>(type: "text", nullable: false),
                    WeatherType = table.Column<string>(type: "text", nullable: true),
                    IsDayTime = table.Column<bool>(type: "boolean", nullable: false),
                    PrecipitationType = table.Column<string>(type: "text", nullable: true),
                    TemperatureInCelsius = table.Column<double>(type: "double precision", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EditDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecords_CreateDate",
                table: "WeatherRecords",
                column: "CreateDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherRecords");
        }
    }
}
