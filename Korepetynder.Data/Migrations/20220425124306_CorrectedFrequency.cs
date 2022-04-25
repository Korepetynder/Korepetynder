using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class CorrectedFrequency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrequencyId",
                table: "TeacherLesson");

            migrationBuilder.DropColumn(
                name: "FrequencyId",
                table: "StudentLesson");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FrequencyId",
                table: "TeacherLesson",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FrequencyId",
                table: "StudentLesson",
                type: "int",
                nullable: true);
        }
    }
}
