using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCAppEntityFramework.Migrations
{
    public partial class ass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "BookModel",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "BookModel",
                keyColumn: "Id",
                keyValue: 1,
                column: "ISBN",
                value: "978-3-16-148410-0");

            migrationBuilder.UpdateData(
                table: "BookModel",
                keyColumn: "Id",
                keyValue: 2,
                column: "ISBN",
                value: "978-3-16-148410-0");

            migrationBuilder.UpdateData(
                table: "BookModel",
                keyColumn: "Id",
                keyValue: 3,
                column: "ISBN",
                value: "978-3-16-148410-0");

            migrationBuilder.UpdateData(
                table: "BookModel",
                keyColumn: "Id",
                keyValue: 4,
                column: "ISBN",
                value: "978-3-16-148410-0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "BookModel");
        }
    }
}
