using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wissance.WeatherControl.Data.Migrations
{
    public partial class Migration_1_Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "MeasureUnit",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Station",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sensor",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasureUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensor_MeasureUnit_MeasureUnitId",
                        column: x => x.MeasureUnitId,
                        principalSchema: "dbo",
                        principalTable: "MeasureUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sensor_Station_StationId",
                        column: x => x.StationId,
                        principalSchema: "dbo",
                        principalTable: "Station",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measurement",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SampleDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SensorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurement_Sensor_SensorId",
                        column: x => x.SensorId,
                        principalSchema: "dbo",
                        principalTable: "Sensor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurement_SensorId",
                schema: "dbo",
                table: "Measurement",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureUnit_Name",
                schema: "dbo",
                table: "MeasureUnit",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_MeasureUnitId",
                schema: "dbo",
                table: "Sensor",
                column: "MeasureUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_Name",
                schema: "dbo",
                table: "Sensor",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_StationId",
                schema: "dbo",
                table: "Sensor",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Station_Name",
                schema: "dbo",
                table: "Station",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurement",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sensor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MeasureUnit",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Station",
                schema: "dbo");
        }
    }
}
