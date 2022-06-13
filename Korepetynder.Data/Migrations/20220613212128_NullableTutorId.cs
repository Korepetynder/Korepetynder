using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class NullableTutorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultimediaFiles_Tutors_TutorId",
                table: "MultimediaFiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "TutorId",
                table: "MultimediaFiles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_MultimediaFiles_Tutors_TutorId",
                table: "MultimediaFiles",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultimediaFiles_Tutors_TutorId",
                table: "MultimediaFiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "TutorId",
                table: "MultimediaFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MultimediaFiles_Tutors_TutorId",
                table: "MultimediaFiles",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
