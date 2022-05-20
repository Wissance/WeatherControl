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
                name: "Station",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                name: "Measurement",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: true),
                    Pressure = table.Column<double>(type: "float", nullable: true),
                    Humidity = table.Column<double>(type: "float", nullable: true),
                    WindSpeed = table.Column<double>(type: "float", nullable: true),
                    StationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurement_Station_StationId",
                        column: x => x.StationId,
                        principalSchema: "dbo",
                        principalTable: "Station",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurement_StationId",
                schema: "dbo",
                table: "Measurement",
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
                name: "Station",
                schema: "dbo");
        }
    }
}
