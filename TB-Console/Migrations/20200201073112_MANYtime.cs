using Microsoft.EntityFrameworkCore.Migrations;

namespace TripBlazrConsole.Migrations
{
    public partial class MANYtime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "TagMenuGroups");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AccountUser");

            migrationBuilder.AlterColumn<bool>(
                name: "SeeWebsite",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Is24Hours",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Inactive",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AccountUser",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "Inactive",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_AccountId",
                table: "Tags",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TagMenuGroups_MenuGroupId",
                table: "TagMenuGroups",
                column: "MenuGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TagMenuGroups_TagId",
                table: "TagMenuGroups",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationTags_LocationId",
                table: "LocationTags",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationTags_TagId",
                table: "LocationTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationCategories_CategoryId",
                table: "LocationCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationCategories_LocationId",
                table: "LocationCategories",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUser_AccountId",
                table: "AccountUser",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUser_ApplicationUserId",
                table: "AccountUser",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUser_Accounts_AccountId",
                table: "AccountUser",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUser_AspNetUsers_ApplicationUserId",
                table: "AccountUser",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationCategories_Categories_CategoryId",
                table: "LocationCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationCategories_Locations_LocationId",
                table: "LocationCategories",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationTags_Locations_LocationId",
                table: "LocationTags",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationTags_Tags_TagId",
                table: "LocationTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TagMenuGroups_MenuGroups_MenuGroupId",
                table: "TagMenuGroups",
                column: "MenuGroupId",
                principalTable: "MenuGroups",
                principalColumn: "MenuGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TagMenuGroups_Tags_TagId",
                table: "TagMenuGroups",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Accounts_AccountId",
                table: "Tags",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountUser_Accounts_AccountId",
                table: "AccountUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountUser_AspNetUsers_ApplicationUserId",
                table: "AccountUser");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationCategories_Categories_CategoryId",
                table: "LocationCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationCategories_Locations_LocationId",
                table: "LocationCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationTags_Locations_LocationId",
                table: "LocationTags");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationTags_Tags_TagId",
                table: "LocationTags");

            migrationBuilder.DropForeignKey(
                name: "FK_TagMenuGroups_MenuGroups_MenuGroupId",
                table: "TagMenuGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_TagMenuGroups_Tags_TagId",
                table: "TagMenuGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Accounts_AccountId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_AccountId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_TagMenuGroups_MenuGroupId",
                table: "TagMenuGroups");

            migrationBuilder.DropIndex(
                name: "IX_TagMenuGroups_TagId",
                table: "TagMenuGroups");

            migrationBuilder.DropIndex(
                name: "IX_LocationTags_LocationId",
                table: "LocationTags");

            migrationBuilder.DropIndex(
                name: "IX_LocationTags_TagId",
                table: "LocationTags");

            migrationBuilder.DropIndex(
                name: "IX_LocationCategories_CategoryId",
                table: "LocationCategories");

            migrationBuilder.DropIndex(
                name: "IX_LocationCategories_LocationId",
                table: "LocationCategories");

            migrationBuilder.DropIndex(
                name: "IX_AccountUser_AccountId",
                table: "AccountUser");

            migrationBuilder.DropIndex(
                name: "IX_AccountUser_ApplicationUserId",
                table: "AccountUser");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AccountUser");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "TagMenuGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "SeeWebsite",
                table: "Locations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Locations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Is24Hours",
                table: "Locations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Inactive",
                table: "Locations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AccountUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "Inactive",
                table: "Accounts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
