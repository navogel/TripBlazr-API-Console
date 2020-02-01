using Microsoft.EntityFrameworkCore.Migrations;

namespace TripBlazrConsole.Migrations
{
    public partial class cascading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationTag_Tag_TagId",
                table: "LocationTag");

            migrationBuilder.DropForeignKey(
                name: "FK_TagMenuGroup_Tag_TagId",
                table: "TagMenuGroup");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationTag_Tag_TagId",
                table: "LocationTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagMenuGroup_Tag_TagId",
                table: "TagMenuGroup",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationTag_Tag_TagId",
                table: "LocationTag");

            migrationBuilder.DropForeignKey(
                name: "FK_TagMenuGroup_Tag_TagId",
                table: "TagMenuGroup");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationTag_Tag_TagId",
                table: "LocationTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TagMenuGroup_Tag_TagId",
                table: "TagMenuGroup",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
