using Microsoft.EntityFrameworkCore.Migrations;

namespace TripBlazrConsole.Migrations
{
    public partial class addedStateToLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Location",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Location");
        }
    }
}
