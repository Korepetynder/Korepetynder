using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentLocationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Locations_ParentLocationId",
                        column: x => x.ParentLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProfilePictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    ProfilePictureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_ProfilePictures_ProfilePictureId",
                        column: x => x.ProfilePictureId,
                        principalTable: "ProfilePictures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentPreferredLocations",
                columns: table => new
                {
                    PreferredLocationsId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPreferredLocations", x => new { x.PreferredLocationsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_StudentPreferredLocations_Locations_PreferredLocationsId",
                        column: x => x.PreferredLocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentPreferredLocations_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentLesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreferredCostMinimum = table.Column<int>(type: "int", nullable: false),
                    PreferredCostMaximum = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLesson", x => x.Id);
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
                name: "MultimediaFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultimediaFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultimediaFiles_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MultimediaFiles_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherLesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherLesson", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "TeacherStudents",
                columns: table => new
                {
                    StudentsId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherStudents", x => new { x.StudentsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_TeacherStudents_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherStudents_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherTeachingLocations",
                columns: table => new
                {
                    TeachersId = table.Column<int>(type: "int", nullable: false),
                    TeachingLocationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherTeachingLocations", x => new { x.TeachersId, x.TeachingLocationsId });
                    table.ForeignKey(
                        name: "FK_TeacherTeachingLocations_Locations_TeachingLocationsId",
                        column: x => x.TeachingLocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherTeachingLocations_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, computedColumnSql: "[FirstName] + ' ' + [LastName]"),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LessonLanguages",
                columns: table => new
                {
                    LanguagesId = table.Column<int>(type: "int", nullable: false),
                    StudentLessonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonLanguages", x => new { x.LanguagesId, x.StudentLessonsId });
                    table.ForeignKey(
                        name: "FK_LessonLanguages_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonLanguages_StudentLesson_StudentLessonsId",
                        column: x => x.StudentLessonsId,
                        principalTable: "StudentLesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonLevels",
                columns: table => new
                {
                    LevelsId = table.Column<int>(type: "int", nullable: false),
                    StudentLessonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonLevels", x => new { x.LevelsId, x.StudentLessonsId });
                    table.ForeignKey(
                        name: "FK_LessonLevels_Levels_LevelsId",
                        column: x => x.LevelsId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonLevels_StudentLesson_StudentLessonsId",
                        column: x => x.StudentLessonsId,
                        principalTable: "StudentLesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Polski" },
                    { 2, "Angielski" },
                    { 3, "Niemiecki" }
                });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "Name", "Weight" },
                values: new object[,]
                {
                    { 1, "Szkoła podstawowa", 1 },
                    { 2, "Liceum", 2 },
                    { 3, "Studia wyższe", 3 }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Name", "ParentLocationId" },
                values: new object[,]
                {
                    { 1, "Warszawa", null },
                    { 4, "Łódź", null },
                    { 5, "Kraków", null }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Matematyka" },
                    { 2, "Informatyka" },
                    { 3, "Chemia" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Name", "ParentLocationId" },
                values: new object[] { 2, "Wilanów", 1 });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Name", "ParentLocationId" },
                values: new object[] { 3, "Śródmieście", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Name",
                table: "Languages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonLanguages_StudentLessonsId",
                table: "LessonLanguages",
                column: "StudentLessonsId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLevels_StudentLessonsId",
                table: "LessonLevels",
                column: "StudentLessonsId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_Name",
                table: "Levels",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ParentLocationId",
                table: "Locations",
                column: "ParentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaFiles_SubjectId",
                table: "MultimediaFiles",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaFiles_TeacherId",
                table: "MultimediaFiles",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLesson_StudentId",
                table: "StudentLesson",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLesson_SubjectId",
                table: "StudentLesson",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPreferredLocations_StudentsId",
                table: "StudentPreferredLocations",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Name",
                table: "Subjects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLesson_SubjectId",
                table: "TeacherLesson",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLesson_TeacherId",
                table: "TeacherLesson",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLessonLanguages_TeacherLessonsId",
                table: "TeacherLessonLanguages",
                column: "TeacherLessonsId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLessonLevels_TeacherLessonsId",
                table: "TeacherLessonLevels",
                column: "TeacherLessonsId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_ProfilePictureId",
                table: "Teachers",
                column: "ProfilePictureId",
                unique: true,
                filter: "[ProfilePictureId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudents_TeachersId",
                table: "TeacherStudents",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherTeachingLocations_TeachingLocationsId",
                table: "TeacherTeachingLocations",
                column: "TeachingLocationsId");

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
            migrationBuilder.DropTable(
                name: "LessonLanguages");

            migrationBuilder.DropTable(
                name: "LessonLevels");

            migrationBuilder.DropTable(
                name: "MultimediaFiles");

            migrationBuilder.DropTable(
                name: "StudentPreferredLocations");

            migrationBuilder.DropTable(
                name: "TeacherLessonLanguages");

            migrationBuilder.DropTable(
                name: "TeacherLessonLevels");

            migrationBuilder.DropTable(
                name: "TeacherStudents");

            migrationBuilder.DropTable(
                name: "TeacherTeachingLocations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "StudentLesson");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "TeacherLesson");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "ProfilePictures");
        }
    }
}
