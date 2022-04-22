using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class AddedWeights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Lengths_LengthId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_LengthId",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lengths",
                table: "Lengths");

            migrationBuilder.DropColumn(
                name: "LengthId",
                table: "Lessons");

            migrationBuilder.RenameTable(
                name: "Lengths",
                newName: "Frequencies");

            migrationBuilder.RenameIndex(
                name: "IX_Lengths_Name",
                table: "Frequencies",
                newName: "IX_Frequencies_Name");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Levels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FrequencyId",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Frequencies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Frequencies",
                table: "Frequencies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_FrequencyId",
                table: "Lessons",
                column: "FrequencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Frequencies_FrequencyId",
                table: "Lessons",
                column: "FrequencyId",
                principalTable: "Frequencies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Frequencies_FrequencyId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_FrequencyId",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Frequencies",
                table: "Frequencies");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "FrequencyId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Frequencies");

            migrationBuilder.RenameTable(
                name: "Frequencies",
                newName: "Lengths");

            migrationBuilder.RenameIndex(
                name: "IX_Frequencies_Name",
                table: "Lengths",
                newName: "IX_Lengths_Name");

            migrationBuilder.AddColumn<int>(
                name: "LengthId",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lengths",
                table: "Lengths",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LengthId",
                table: "Lessons",
                column: "LengthId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Lengths_LengthId",
                table: "Lessons",
                column: "LengthId",
                principalTable: "Lengths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
