using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCFinal.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Case",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Case", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Case_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Case_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CaseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceTask_Case_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Case",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "Username" },
                values: new object[] { 1, "Viktor" });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "Username" },
                values: new object[] { 2, "Sigurd" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "This is just a sample product", "Sample Product" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Cool product man", "The Cooler Product" });

            migrationBuilder.InsertData(
                table: "Case",
                columns: new[] { "Id", "ClientId", "Description", "ProductId", "Type" },
                values: new object[] { 1, 1, "Uncool the cool product please", 2, 0 });

            migrationBuilder.InsertData(
                table: "Case",
                columns: new[] { "Id", "ClientId", "Description", "ProductId", "Type" },
                values: new object[] { 2, 2, "Make this sample product into a real one", 1, 1 });

            migrationBuilder.InsertData(
                table: "ResourceTask",
                columns: new[] { "Id", "CaseId", "Description", "Status" },
                values: new object[] { 1, 1, "Uncool the product with barbeque tongs", 2 });

            migrationBuilder.InsertData(
                table: "ResourceTask",
                columns: new[] { "Id", "CaseId", "Description", "Status" },
                values: new object[] { 2, 1, "Finalizations", 1 });

            migrationBuilder.InsertData(
                table: "ResourceTask",
                columns: new[] { "Id", "CaseId", "Description", "Status" },
                values: new object[] { 3, 2, "Do the Magic Mike", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Case_ClientId",
                table: "Case",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Case_ProductId",
                table: "Case",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceTask_CaseId",
                table: "ResourceTask",
                column: "CaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceTask");

            migrationBuilder.DropTable(
                name: "Case");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
