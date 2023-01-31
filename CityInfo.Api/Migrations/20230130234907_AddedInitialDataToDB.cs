using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.Api.Migrations
{
    public partial class AddedInitialDataToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 1,
                column: "CityId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 2,
                column: "CityId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 3,
                column: "CityId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 4,
                column: "CityId",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 1,
                column: "CityId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 2,
                column: "CityId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 3,
                column: "CityId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 4,
                column: "CityId",
                value: 0);
        }
    }
}
