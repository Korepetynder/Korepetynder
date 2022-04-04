using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class SmallChangesToDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Teachers_TeacherId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Teachers_TeacherId1",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Files_ProfilePictureId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locations_UserLocationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_ProfilePictureId",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureId",
                table: "Teachers",
                newName: "PictureId");

            migrationBuilder.RenameColumn(
                name: "TeacherId1",
                table: "Files",
                newName: "Owner2Id");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Files",
                newName: "Owner1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Files_TeacherId1",
                table: "Files",
                newName: "IX_Files_Owner2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Files_TeacherId",
                table: "Files",
                newName: "IX_Files_Owner1Id");

            migrationBuilder.AlterColumn<int>(
                name: "UserLocationId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_PictureId",
                table: "Teachers",
                column: "PictureId",
                unique: true,
                filter: "[PictureId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Teachers_Owner1Id",
                table: "Files",
                column: "Owner1Id",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Teachers_Owner2Id",
                table: "Files",
                column: "Owner2Id",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Files_PictureId",
                table: "Teachers",
                column: "PictureId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locations_UserLocationId",
                table: "Users",
                column: "UserLocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Teachers_Owner1Id",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Teachers_Owner2Id",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Files_PictureId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locations_UserLocationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_PictureId",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "PictureId",
                table: "Teachers",
                newName: "ProfilePictureId");

            migrationBuilder.RenameColumn(
                name: "Owner2Id",
                table: "Files",
                newName: "TeacherId1");

            migrationBuilder.RenameColumn(
                name: "Owner1Id",
                table: "Files",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_Owner2Id",
                table: "Files",
                newName: "IX_Files_TeacherId1");

            migrationBuilder.RenameIndex(
                name: "IX_Files_Owner1Id",
                table: "Files",
                newName: "IX_Files_TeacherId");

            migrationBuilder.AlterColumn<int>(
                name: "UserLocationId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_ProfilePictureId",
                table: "Teachers",
                column: "ProfilePictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Teachers_TeacherId",
                table: "Files",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Teachers_TeacherId1",
                table: "Files",
                column: "TeacherId1",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Files_ProfilePictureId",
                table: "Teachers",
                column: "ProfilePictureId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locations_UserLocationId",
                table: "Users",
                column: "UserLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
