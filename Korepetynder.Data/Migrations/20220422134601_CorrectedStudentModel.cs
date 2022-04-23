using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class CorrectedStudentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreferredCost",
                table: "Students",
                newName: "PreferredCostMinimum");

            migrationBuilder.AddColumn<int>(
                name: "PreferredCostMaximum",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredCostMaximum",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "PreferredCostMinimum",
                table: "Students",
                newName: "PreferredCost");
        }
    }
}
