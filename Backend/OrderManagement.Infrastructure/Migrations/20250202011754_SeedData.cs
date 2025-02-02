using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"), "john.doe@example.com", "John Doe", "123-456-7890" },
                    { new Guid("ee4e3b6e-123d-4ef0-b44d-3cb2217bb48a"), "jane.smith@example.com", "Jane Smith", "098-765-4321" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { new Guid("3db07c1e-94b2-41fa-b9c3-8a8cc6a4fba1"), new Guid("ee4e3b6e-123d-4ef0-b44d-3cb2217bb48a"), new DateTime(2025, 2, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 0, 450.00m },
                    { new Guid("b3475369-74bb-4f50-b17e-d50a3a3c09c7"), new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"), new DateTime(2025, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 0, 250.00m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"), "Product B", 150.00m },
                    { new Guid("f04acbb2-9c97-4fae-853b-e1c1e52715a2"), "Product A", 100.00m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "ProductName", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("8979bfc9-7746-42c5-ae2f-d1e349dd96ff"), new Guid("b3475369-74bb-4f50-b17e-d50a3a3c09c7"), new Guid("f04acbb2-9c97-4fae-853b-e1c1e52715a2"), "Product A", 2, 100.00m },
                    { new Guid("d3158cfc-72e9-43a7-9b5e-34e2d9a55f61"), new Guid("3db07c1e-94b2-41fa-b9c3-8a8cc6a4fba1"), new Guid("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"), "Product B", 3, 150.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("ee4e3b6e-123d-4ef0-b44d-3cb2217bb48a"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("8979bfc9-7746-42c5-ae2f-d1e349dd96ff"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("d3158cfc-72e9-43a7-9b5e-34e2d9a55f61"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f04acbb2-9c97-4fae-853b-e1c1e52715a2"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("3db07c1e-94b2-41fa-b9c3-8a8cc6a4fba1"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("b3475369-74bb-4f50-b17e-d50a3a3c09c7"));
        }
    }
}
