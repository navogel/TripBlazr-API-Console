using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripBlazrConsole.Migrations
{
    public partial class addedLocationCreatedEditedDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Location",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEdited",
                table: "Location",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "DateEdited",
                table: "Location");
        }
    }
}
