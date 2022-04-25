using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name", "TeacherLessonId" },
                values: new object[,]
                {
                    { 1, "Polski", null },
                    { 2, "Angielski", null },
                    { 3, "Niemiecki", null }
                });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "Name", "TeacherLessonId", "Weight" },
                values: new object[,]
                {
                    { 1, "Szkoła podstawowa", null, 1 },
                    { 2, "Liceum", null, 2 },
                    { 3, "Studia wyższe", null, 3 }
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
