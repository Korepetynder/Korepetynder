using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class AddFavouriteStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorStudent");

            migrationBuilder.CreateTable(
                name: "DiscardedTutorStudents",
                columns: table => new
                {
                    DiscardedByStudentsUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscardedTutorsUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscardedTutorStudents", x => new { x.DiscardedByStudentsUserId, x.DiscardedTutorsUserId });
                    table.ForeignKey(
                        name: "FK_DiscardedTutorStudents_Students_DiscardedByStudentsUserId",
                        column: x => x.DiscardedByStudentsUserId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscardedTutorStudents_Tutors_DiscardedTutorsUserId",
                        column: x => x.DiscardedTutorsUserId,
                        principalTable: "Tutors",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteTutorStudents",
                columns: table => new
                {
                    FavouriteTutorsUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FavouritedByStudentsUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteTutorStudents", x => new { x.FavouriteTutorsUserId, x.FavouritedByStudentsUserId });
                    table.ForeignKey(
                        name: "FK_FavouriteTutorStudents_Students_FavouritedByStudentsUserId",
                        column: x => x.FavouritedByStudentsUserId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteTutorStudents_Tutors_FavouriteTutorsUserId",
                        column: x => x.FavouriteTutorsUserId,
                        principalTable: "Tutors",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscardedTutorStudents_DiscardedTutorsUserId",
                table: "DiscardedTutorStudents",
                column: "DiscardedTutorsUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteTutorStudents_FavouritedByStudentsUserId",
                table: "FavouriteTutorStudents",
                column: "FavouritedByStudentsUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscardedTutorStudents");

            migrationBuilder.DropTable(
                name: "FavouriteTutorStudents");

            migrationBuilder.CreateTable(
                name: "TutorStudent",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorStudent", x => new { x.StudentId, x.TutorId });
                    table.CheckConstraint("CK_StudentId_TutorId", "[TutorId] != [StudentId]");
                    table.ForeignKey(
                        name: "FK_TutorStudent_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorStudent_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutorStudent_TutorId",
                table: "TutorStudent",
                column: "TutorId");
        }
    }
}
