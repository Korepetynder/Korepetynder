using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class CorrectedTeacherLessonLanguageRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_TeacherLesson_TeacherLessonId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonLanguages_StudentLesson_LessonsId",
                table: "LessonLanguages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_TeacherLessonId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "TeacherLessonId",
                table: "Languages");

            migrationBuilder.RenameColumn(
                name: "LessonsId",
                table: "LessonLanguages",
                newName: "StudentLessonsId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonLanguages_LessonsId",
                table: "LessonLanguages",
                newName: "IX_LessonLanguages_StudentLessonsId");

            migrationBuilder.CreateTable(
                name: "TeacherLessonLanguages",
                columns: table => new
                {
                    LanguagesId = table.Column<int>(type: "int", nullable: false),
                    TeacherLessonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherLessonLanguages", x => new { x.LanguagesId, x.TeacherLessonsId });
                    table.ForeignKey(
                        name: "FK_TeacherLessonLanguages_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherLessonLanguages_TeacherLesson_TeacherLessonsId",
                        column: x => x.TeacherLessonsId,
                        principalTable: "TeacherLesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLessonLanguages_TeacherLessonsId",
                table: "TeacherLessonLanguages",
                column: "TeacherLessonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonLanguages_StudentLesson_StudentLessonsId",
                table: "LessonLanguages",
                column: "StudentLessonsId",
                principalTable: "StudentLesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonLanguages_StudentLesson_StudentLessonsId",
                table: "LessonLanguages");

            migrationBuilder.DropTable(
                name: "TeacherLessonLanguages");

            migrationBuilder.RenameColumn(
                name: "StudentLessonsId",
                table: "LessonLanguages",
                newName: "LessonsId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonLanguages_StudentLessonsId",
                table: "LessonLanguages",
                newName: "IX_LessonLanguages_LessonsId");

            migrationBuilder.AddColumn<int>(
                name: "TeacherLessonId",
                table: "Languages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_TeacherLessonId",
                table: "Languages",
                column: "TeacherLessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_TeacherLesson_TeacherLessonId",
                table: "Languages",
                column: "TeacherLessonId",
                principalTable: "TeacherLesson",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonLanguages_StudentLesson_LessonsId",
                table: "LessonLanguages",
                column: "LessonsId",
                principalTable: "StudentLesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
