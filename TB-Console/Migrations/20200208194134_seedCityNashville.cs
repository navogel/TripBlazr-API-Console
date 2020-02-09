using Microsoft.EntityFrameworkCore.Migrations;

namespace TripBlazrConsole.Migrations
{
    public partial class seedCityNashville : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountId", "City", "CitySlug", "Inactive", "IsDeleted", "Latitude", "Longitude", "Name" },
                values: new object[] { 1, "Nashville", "this-is-nashville-slug", false, false, 36.162700000000001, 86.781599999999997, "NCVC" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 1);
        }
    }
}
