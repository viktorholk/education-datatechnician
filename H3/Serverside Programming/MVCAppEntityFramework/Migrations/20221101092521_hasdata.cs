using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCAppEntityFramework.Migrations
{
    public partial class hasdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookModel",
                columns: new[] { "Id", "Author", "Category", "Description", "Price", "Publisher", "Title", "TotalPages" },
                values: new object[] { 1, "Viktor", "Cooking", "This book is about cooking.", 299.89999999999998, "freePublishNet", "Holk Smash 4", 180 });

            migrationBuilder.InsertData(
                table: "BookModel",
                columns: new[] { "Id", "Author", "Category", "Description", "Price", "Publisher", "Title", "TotalPages" },
                values: new object[] { 2, "SAS", "Airplanes", "This book is about airplanes", 100.0, "SAS Publishers", "How to build a sinking boat", 10 });

            migrationBuilder.InsertData(
                table: "BookModel",
                columns: new[] { "Id", "Author", "Category", "Description", "Price", "Publisher", "Title", "TotalPages" },
                values: new object[] { 3, "Homeless Ben", "Aerodynamics", "How drinking keeps me sane", 599.89999999999998, "Homeless Ben", "Keep Drinking Alcohol", 200 });

            migrationBuilder.InsertData(
                table: "BookModel",
                columns: new[] { "Id", "Author", "Category", "Description", "Price", "Publisher", "Title", "TotalPages" },
                values: new object[] { 4, "Richard", "Comedy", "No fact checks", 299.89999999999998, "StackOverflow", "Why HTML is a programming language", 180 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookModel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookModel",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookModel",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookModel",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
