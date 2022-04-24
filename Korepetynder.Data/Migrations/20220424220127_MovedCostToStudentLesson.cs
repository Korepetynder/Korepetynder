using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class MovedCostToStudentLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredCostMaximum",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PreferredCostMinimum",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "PreferredCostMaximum",
                table: "StudentLesson",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreferredCostMinimum",
                table: "StudentLesson",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredCostMaximum",
                table: "StudentLesson");

            migrationBuilder.DropColumn(
                name: "PreferredCostMinimum",
                table: "StudentLesson");

            migrationBuilder.AddColumn<int>(
                name: "PreferredCostMaximum",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreferredCostMinimum",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
