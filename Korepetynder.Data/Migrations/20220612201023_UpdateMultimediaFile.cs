using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class UpdateMultimediaFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultimediaFiles_Subjects_SubjectId",
                table: "MultimediaFiles");

            migrationBuilder.DropIndex(
                name: "IX_MultimediaFiles_SubjectId",
                table: "MultimediaFiles");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "MultimediaFiles");

            migrationBuilder.CreateTable(
                name: "TutorLessonMultimediaFiles",
                columns: table => new
                {
                    MultimediaFileId = table.Column<int>(type: "int", nullable: false),
                    TutorLessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorLessonMultimediaFiles", x => new { x.MultimediaFileId, x.TutorLessonId });
                    table.ForeignKey(
                        name: "FK_TutorLessonMultimediaFiles_MultimediaFiles_MultimediaFileId",
                        column: x => x.MultimediaFileId,
                        principalTable: "MultimediaFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorLessonMultimediaFiles_TutorLessons_TutorLessonId",
                        column: x => x.TutorLessonId,
                        principalTable: "TutorLessons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutorLessonMultimediaFiles_TutorLessonId",
                table: "TutorLessonMultimediaFiles",
                column: "TutorLessonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorLessonMultimediaFiles");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "MultimediaFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaFiles_SubjectId",
                table: "MultimediaFiles",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MultimediaFiles_Subjects_SubjectId",
                table: "MultimediaFiles",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }
    }
}
