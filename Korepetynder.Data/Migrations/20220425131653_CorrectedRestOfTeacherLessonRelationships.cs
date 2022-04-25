using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class CorrectedRestOfTeacherLessonRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonLevels_StudentLesson_LessonsId",
                table: "LessonLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_Levels_TeacherLesson_TeacherLessonId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Levels_TeacherLessonId",
                table: "Levels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonLevels",
                table: "LessonLevels");

            migrationBuilder.DropIndex(
                name: "IX_LessonLevels_LevelsId",
                table: "LessonLevels");

            migrationBuilder.DropColumn(
                name: "TeacherLessonId",
                table: "Levels");

            migrationBuilder.RenameColumn(
                name: "LessonsId",
                table: "LessonLevels",
                newName: "StudentLessonsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonLevels",
                table: "LessonLevels",
                columns: new[] { "LevelsId", "StudentLessonsId" });

            migrationBuilder.CreateTable(
                name: "TeacherLessonLevels",
                columns: table => new
                {
                    LevelsId = table.Column<int>(type: "int", nullable: false),
                    TeacherLessonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherLessonLevels", x => new { x.LevelsId, x.TeacherLessonsId });
                    table.ForeignKey(
                        name: "FK_TeacherLessonLevels_Levels_LevelsId",
                        column: x => x.LevelsId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherLessonLevels_TeacherLesson_TeacherLessonsId",
                        column: x => x.TeacherLessonsId,
                        principalTable: "TeacherLesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonLevels_StudentLessonsId",
                table: "LessonLevels",
                column: "StudentLessonsId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLessonLevels_TeacherLessonsId",
                table: "TeacherLessonLevels",
                column: "TeacherLessonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonLevels_StudentLesson_StudentLessonsId",
                table: "LessonLevels",
                column: "StudentLessonsId",
                principalTable: "StudentLesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonLevels_StudentLesson_StudentLessonsId",
                table: "LessonLevels");

            migrationBuilder.DropTable(
                name: "TeacherLessonLevels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonLevels",
                table: "LessonLevels");

            migrationBuilder.DropIndex(
                name: "IX_LessonLevels_StudentLessonsId",
                table: "LessonLevels");

            migrationBuilder.RenameColumn(
                name: "StudentLessonsId",
                table: "LessonLevels",
                newName: "LessonsId");

            migrationBuilder.AddColumn<int>(
                name: "TeacherLessonId",
                table: "Levels",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonLevels",
                table: "LessonLevels",
                columns: new[] { "LessonsId", "LevelsId" });

            migrationBuilder.CreateIndex(
                name: "IX_Levels_TeacherLessonId",
                table: "Levels",
                column: "TeacherLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLevels_LevelsId",
                table: "LessonLevels",
                column: "LevelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonLevels_StudentLesson_LessonsId",
                table: "LessonLevels",
                column: "LessonsId",
                principalTable: "StudentLesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_TeacherLesson_TeacherLessonId",
                table: "Levels",
                column: "TeacherLessonId",
                principalTable: "TeacherLesson",
                principalColumn: "Id");
        }
    }
}
