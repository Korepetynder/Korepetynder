using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class UpdateUserAndFrequency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentLesson_Frequencies_FrequencyId",
                table: "StudentLesson");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLesson_Frequencies_FrequencyId",
                table: "TeacherLesson");

            migrationBuilder.DropTable(
                name: "Frequencies");

            migrationBuilder.DropIndex(
                name: "IX_Users_StudentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TeacherId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TeacherLesson_FrequencyId",
                table: "TeacherLesson");

            migrationBuilder.DropIndex(
                name: "IX_StudentLesson_FrequencyId",
                table: "StudentLesson");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "TelephoneNumber",
                table: "Users",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "TeacherLesson",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "StudentLesson",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_StudentId",
                table: "Users",
                column: "StudentId",
                unique: true,
                filter: "[StudentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeacherId",
                table: "Users",
                column: "TeacherId",
                unique: true,
                filter: "[TeacherId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_StudentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TeacherId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TelephoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "TeacherLesson");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "StudentLesson");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "UserName");

            migrationBuilder.CreateTable(
                name: "Frequencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequencies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_StudentId",
                table: "Users",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeacherId",
                table: "Users",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLesson_FrequencyId",
                table: "TeacherLesson",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLesson_FrequencyId",
                table: "StudentLesson",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Frequencies_Name",
                table: "Frequencies",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLesson_Frequencies_FrequencyId",
                table: "StudentLesson",
                column: "FrequencyId",
                principalTable: "Frequencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLesson_Frequencies_FrequencyId",
                table: "TeacherLesson",
                column: "FrequencyId",
                principalTable: "Frequencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
