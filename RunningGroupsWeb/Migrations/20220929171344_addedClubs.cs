using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunningGroupsWeb.Migrations
{
    public partial class addedClubs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Club_Address_AddressId",
                table: "Club");

            migrationBuilder.DropForeignKey(
                name: "FK_Club_AppUser_AppUserId",
                table: "Club");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Club",
                table: "Club");

            migrationBuilder.RenameTable(
                name: "Club",
                newName: "Clubs");

            migrationBuilder.RenameIndex(
                name: "IX_Club_AppUserId",
                table: "Clubs",
                newName: "IX_Clubs_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Club_AddressId",
                table: "Clubs",
                newName: "IX_Clubs_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clubs",
                table: "Clubs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Address_AddressId",
                table: "Clubs",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_AppUser_AppUserId",
                table: "Clubs",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Address_AddressId",
                table: "Clubs");

            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_AppUser_AppUserId",
                table: "Clubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clubs",
                table: "Clubs");

            migrationBuilder.RenameTable(
                name: "Clubs",
                newName: "Club");

            migrationBuilder.RenameIndex(
                name: "IX_Clubs_AppUserId",
                table: "Club",
                newName: "IX_Club_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Clubs_AddressId",
                table: "Club",
                newName: "IX_Club_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Club",
                table: "Club",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Club_Address_AddressId",
                table: "Club",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Club_AppUser_AppUserId",
                table: "Club",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id");
        }
    }
}
