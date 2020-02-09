using Microsoft.EntityFrameworkCore.Migrations;

namespace TripBlazrConsole.Migrations
{
    public partial class changedInactiveToIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inactive",
                table: "Location");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Location",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Location");

            migrationBuilder.AddColumn<bool>(
                name: "Inactive",
                table: "Location",
                type: "bit",
                nullable: true);
        }
    }
}
