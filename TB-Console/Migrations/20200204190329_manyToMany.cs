using Microsoft.EntityFrameworkCore.Migrations;

namespace TripBlazrConsole.Migrations
{
    public partial class manyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Inactive = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CitySlug = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "AccountUser",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUser", x => new { x.AccountId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_AccountUser_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountUser_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    ShortSummary = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    SortId = table.Column<int>(nullable: true),
                    VideoId = table.Column<string>(nullable: true),
                    VideoStartTime = table.Column<int>(nullable: true),
                    VideoEndTime = table.Column<int>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Inactive = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Zipcode = table.Column<int>(nullable: true),
                    Is24Hours = table.Column<bool>(nullable: true),
                    SeeWebsite = table.Column<bool>(nullable: true),
                    HoursNotes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Location_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuGroup",
                columns: table => new
                {
                    MenuGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SortId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuGroup", x => x.MenuGroupId);
                    table.ForeignKey(
                        name: "FK_MenuGroup_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_Tag_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hours",
                columns: table => new
                {
                    HoursId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(nullable: false),
                    DayCode = table.Column<int>(nullable: false),
                    Open = table.Column<string>(nullable: false),
                    Close = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hours", x => x.HoursId);
                    table.ForeignKey(
                        name: "FK_Hours_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationCategory",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationCategory", x => new { x.LocationId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_LocationCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationCategory_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationTag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationTag", x => new { x.LocationId, x.TagId });
                    table.ForeignKey(
                        name: "FK_LocationTag_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagMenuGroup",
                columns: table => new
                {
                    MenuGroupId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagMenuGroup", x => new { x.TagId, x.MenuGroupId });
                    table.ForeignKey(
                        name: "FK_TagMenuGroup_MenuGroup_MenuGroupId",
                        column: x => x.MenuGroupId,
                        principalTable: "MenuGroup",
                        principalColumn: "MenuGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagMenuGroup_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountUser_ApplicationUserId",
                table: "AccountUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Hours_LocationId",
                table: "Hours",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_AccountId",
                table: "Location",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationCategory_CategoryId",
                table: "LocationCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationTag_TagId",
                table: "LocationTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuGroup_AccountId",
                table: "MenuGroup",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_AccountId",
                table: "Tag",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TagMenuGroup_MenuGroupId",
                table: "TagMenuGroup",
                column: "MenuGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountUser");

            migrationBuilder.DropTable(
                name: "Hours");

            migrationBuilder.DropTable(
                name: "LocationCategory");

            migrationBuilder.DropTable(
                name: "LocationTag");

            migrationBuilder.DropTable(
                name: "TagMenuGroup");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "MenuGroup");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
