using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OBAPI.Web.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    BankID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Branches_Banks_BankID",
                        column: x => x.BankID,
                        principalTable: "Banks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    BrachID = table.Column<int>(nullable: false),
                    BranchID = table.Column<int>(nullable: true),
                    CustomerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customers_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountPostings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    AccountID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPostings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountPostings_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "ID", "Name", "Number" },
                values: new object[] { 1, "OBAPI Bank", 123 });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "ID", "BrachID", "BranchID", "Code", "CustomerID", "Name" },
                values: new object[] { 1, 2, null, "32791181130", null, "Alice Smith" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "ID", "BrachID", "BranchID", "Code", "CustomerID", "Name" },
                values: new object[] { 2, 2, null, "22691181130", null, "Bob Smith" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "ID", "Category", "CustomerID", "Number" },
                values: new object[,]
                {
                    { 1, "Checking", 1, 818181 },
                    { 2, "Checking", 2, 616161 }
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "ID", "BankID", "Name", "Number" },
                values: new object[,]
                {
                    { 1, 1, "Brach One", 1000 },
                    { 2, 1, "Brach Two", 2000 }
                });

            migrationBuilder.InsertData(
                table: "AccountPostings",
                columns: new[] { "ID", "AccountID", "Amount", "Date", "Description" },
                values: new object[] { 1, 1, 1521m, new DateTime(2019, 11, 22, 16, 44, 45, 890, DateTimeKind.Local).AddTicks(3999), "Deposito em caixa" });

            migrationBuilder.InsertData(
                table: "AccountPostings",
                columns: new[] { "ID", "AccountID", "Amount", "Date", "Description" },
                values: new object[] { 2, 1, -11m, new DateTime(2019, 11, 29, 16, 44, 45, 893, DateTimeKind.Local).AddTicks(6451), "Cobrança de Taxa" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountPostings_AccountID",
                table: "AccountPostings",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerID",
                table: "Accounts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_BankID",
                table: "Branches",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BranchID",
                table: "Customers",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerID",
                table: "Customers",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountPostings");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Banks");
        }
    }
}
