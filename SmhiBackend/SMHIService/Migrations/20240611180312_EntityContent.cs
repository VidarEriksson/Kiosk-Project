using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMHIService.Migrations
{
    /// <inheritdoc />
    public partial class EntityContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Observations",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Observations",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "MeasuredDate",
                table: "Observations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "MeasuredValue",
                table: "Observations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "StationKey",
                table: "Observations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "StationName",
                table: "Observations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Updated",
                table: "Observations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<double>(
                name: "CloudCover",
                table: "Forecasts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Humidity",
                table: "Forecasts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Forecasts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Forecasts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Precipitation",
                table: "Forecasts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Pressure",
                table: "Forecasts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Symbol",
                table: "Forecasts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "Forecasts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ValidTime",
                table: "Forecasts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<double>(
                name: "WindDirection",
                table: "Forecasts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WindSpeed",
                table: "Forecasts",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Observations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Observations");

            migrationBuilder.DropColumn(
                name: "MeasuredDate",
                table: "Observations");

            migrationBuilder.DropColumn(
                name: "MeasuredValue",
                table: "Observations");

            migrationBuilder.DropColumn(
                name: "StationKey",
                table: "Observations");

            migrationBuilder.DropColumn(
                name: "StationName",
                table: "Observations");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Observations");

            migrationBuilder.DropColumn(
                name: "CloudCover",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Humidity",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Precipitation",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Pressure",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "ValidTime",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "WindDirection",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "WindSpeed",
                table: "Forecasts");
        }
    }
}
