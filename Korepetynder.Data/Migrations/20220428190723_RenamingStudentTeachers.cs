using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class RenamingStudentTeachers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherStudents_Teachers_TeachersId",
                table: "TeacherStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherStudents",
                table: "TeacherStudents");

            migrationBuilder.DropIndex(
                name: "IX_TeacherStudents_TeachersId",
                table: "TeacherStudents");

            migrationBuilder.RenameColumn(
                name: "TeachersId",
                table: "TeacherStudents",
                newName: "DiscardedTeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherStudents",
                table: "TeacherStudents",
                columns: new[] { "DiscardedTeacherId", "StudentsId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudents_StudentsId",
                table: "TeacherStudents",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherStudents_Teachers_DiscardedTeacherId",
                table: "TeacherStudents",
                column: "DiscardedTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherStudents_Teachers_DiscardedTeacherId",
                table: "TeacherStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherStudents",
                table: "TeacherStudents");

            migrationBuilder.DropIndex(
                name: "IX_TeacherStudents_StudentsId",
                table: "TeacherStudents");

            migrationBuilder.RenameColumn(
                name: "DiscardedTeacherId",
                table: "TeacherStudents",
                newName: "TeachersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherStudents",
                table: "TeacherStudents",
                columns: new[] { "StudentsId", "TeachersId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudents_TeachersId",
                table: "TeacherStudents",
                column: "TeachersId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherStudents_Teachers_TeachersId",
                table: "TeacherStudents",
                column: "TeachersId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
