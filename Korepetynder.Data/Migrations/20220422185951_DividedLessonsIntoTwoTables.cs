using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class DividedLessonsIntoTwoTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonLanguages_Lessons_LessonsId",
                table: "LessonLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonLevels_Lessons_LessonsId",
                table: "LessonLevels");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Teachers");

            migrationBuilder.AddColumn<int>(
                name: "TeacherLessonId",
                table: "Levels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherLessonId",
                table: "Languages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentLesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrequencyId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentLesson_Frequencies_FrequencyId",
                        column: x => x.FrequencyId,
                        principalTable: "Frequencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentLesson_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentLesson_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherLesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrequencyId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherLesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherLesson_Frequencies_FrequencyId",
                        column: x => x.FrequencyId,
                        principalTable: "Frequencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherLesson_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherLesson_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Levels_TeacherLessonId",
                table: "Levels",
                column: "TeacherLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_TeacherLessonId",
                table: "Languages",
                column: "TeacherLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLesson_FrequencyId",
                table: "StudentLesson",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLesson_StudentId",
                table: "StudentLesson",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLesson_SubjectId",
                table: "StudentLesson",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLesson_FrequencyId",
                table: "TeacherLesson",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLesson_SubjectId",
                table: "TeacherLesson",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLesson_TeacherId",
                table: "TeacherLesson",
                column: "TeacherId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_TeacherLesson_TeacherLessonId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonLanguages_StudentLesson_LessonsId",
                table: "LessonLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonLevels_StudentLesson_LessonsId",
                table: "LessonLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_Levels_TeacherLesson_TeacherLessonId",
                table: "Levels");

            migrationBuilder.DropTable(
                name: "StudentLesson");

            migrationBuilder.DropTable(
                name: "TeacherLesson");

            migrationBuilder.DropIndex(
                name: "IX_Levels_TeacherLessonId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Languages_TeacherLessonId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "TeacherLessonId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "TeacherLessonId",
                table: "Languages");

            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrequencyId = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.CheckConstraint("CK_Lesson_StudentId_TeacherId", "[StudentId] IS NULL OR [TeacherId] IS NULL");
                    table.ForeignKey(
                        name: "FK_Lessons_Frequencies_FrequencyId",
                        column: x => x.FrequencyId,
                        principalTable: "Frequencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lessons_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lessons_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_FrequencyId",
                table: "Lessons",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_StudentId",
                table: "Lessons",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SubjectId",
                table: "Lessons",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TeacherId",
                table: "Lessons",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonLanguages_Lessons_LessonsId",
                table: "LessonLanguages",
                column: "LessonsId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonLevels_Lessons_LessonsId",
                table: "LessonLevels",
                column: "LessonsId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
