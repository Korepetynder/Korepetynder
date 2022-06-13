using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korepetynder.Data.Migrations
{
    public partial class UpdateInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1,
                column: "WasAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2,
                column: "WasAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3,
                column: "WasAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 1,
                column: "WasAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 2,
                column: "WasAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 3,
                column: "WasAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1,
                column: "WasAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2,
                column: "WasAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3,
                column: "WasAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 4,
                column: "WasAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 5,
                column: "WasAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1,
                column: "WasAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2,
                column: "WasAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3,
                column: "WasAccepted",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1,
                column: "WasAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2,
                column: "WasAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3,
                column: "WasAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 1,
                column: "WasAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 2,
                column: "WasAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 3,
                column: "WasAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1,
                column: "WasAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2,
                column: "WasAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3,
                column: "WasAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 4,
                column: "WasAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 5,
                column: "WasAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1,
                column: "WasAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2,
                column: "WasAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3,
                column: "WasAccepted",
                value: false);
        }
    }
}
