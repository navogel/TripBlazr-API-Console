using Microsoft.EntityFrameworkCore.Migrations;

namespace TripBlazrConsole.Migrations
{
    public partial class adjustedLocationAndHourTable24HourOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is24Hours",
                table: "Location");

            migrationBuilder.AlterColumn<string>(
                name: "Open",
                table: "Hours",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Close",
                table: "Hours",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Is24Hours",
                table: "Hours",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is24Hours",
                table: "Hours");

            migrationBuilder.AddColumn<bool>(
                name: "Is24Hours",
                table: "Location",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Open",
                table: "Hours",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Close",
                table: "Hours",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
