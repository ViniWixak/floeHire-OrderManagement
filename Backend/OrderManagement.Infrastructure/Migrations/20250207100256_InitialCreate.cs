using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[] { new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"), "john.doe@example.com", "John Doe", "123-456-7890" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { new Guid("3db07c1e-94b2-41fa-b9c3-8a8cc6a4fba1"), new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"), new DateTime(2025, 2, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 0, 78.00m },
                    { new Guid("766aba8f-8c06-48a0-a939-41879f27be5a"), new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"), new DateTime(2025, 2, 2, 14, 7, 7, 1, DateTimeKind.Unspecified), 0, 76.00m },
                    { new Guid("99b6bce5-9884-461b-9a45-8cfeb66b8804"), new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"), new DateTime(2025, 2, 2, 1, 53, 26, 132, DateTimeKind.Unspecified), 0, 128.00m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("19bb2513-fedf-44b6-c44c-08dd43d9cd34"), "Calabresa", 24.00m },
                    { new Guid("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"), "Marguerita", 26.00m },
                    { new Guid("f04acbb2-9c97-4fae-853b-e1c1e52715a2"), "Quatro queijos", 40.00m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "ProductName", "Quantity", "TotalPrice", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("11b23121-17ed-4b15-9dd2-2a9dc3cc288c"), new Guid("766aba8f-8c06-48a0-a939-41879f27be5a"), new Guid("19bb2513-fedf-44b6-c44c-08dd43d9cd34"), "Calabresa", 1, 24.00m, 24.00m },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new Guid("766aba8f-8c06-48a0-a939-41879f27be5a"), new Guid("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"), "Marguerita", 2, 52.00m, 26.00m },
                    { new Guid("3fa85f64-5717-4562-c3fc-3c963f66afa6"), new Guid("99b6bce5-9884-461b-9a45-8cfeb66b8804"), new Guid("f04acbb2-9c97-4fae-853b-e1c1e52715a2"), "Quatro queijos", 2, 80.00m, 40.00m },
                    { new Guid("c66cfce9-b460-4ad5-b7c3-f6f70370fb99"), new Guid("99b6bce5-9884-461b-9a45-8cfeb66b8804"), new Guid("19bb2513-fedf-44b6-c44c-08dd43d9cd34"), "Calabresa", 2, 48.00m, 24.00m },
                    { new Guid("d3158cfc-72e9-43a7-9b5e-34e2d9a55f61"), new Guid("3db07c1e-94b2-41fa-b9c3-8a8cc6a4fba1"), new Guid("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"), "Marguerita", 3, 78.00m, 26.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
