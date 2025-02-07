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
                    { new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"), "john.doe@example.com", "John Doe", "123-456-7890" }
                });

            // Inserir dados na tabela Orders
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { new Guid("766aba8f-8c06-48a0-a939-41879f27be5a"), new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"), new DateTime(2025, 2, 2, 14, 7, 7, 1), 1, 76.00m },
                    { new Guid("3db07c1e-94b2-41fa-b9c3-8a8cc6a4fba1"), new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"), new DateTime(2025, 2, 1, 13, 0, 0, 0), 3, 78.00m },
                    { new Guid("99b6bce5-9884-461b-9a45-8cfeb66b8804"), new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"), new DateTime(2025, 2, 2, 1, 53, 26, 132), 2, 128.00m }
                });

            // Inserir dados na tabela Products
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("19bb2513-fedf-44b6-c44c-08dd43d9cd34"), "Calabresa", 24.00m },
                    { new Guid("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"), "Marguerita", 26.00m },
                    { new Guid("f04acbb2-9c97-4fae-853b-e1c1e52715a2"), "Quatro queijos", 40.00m }
                });

            // Inserir dados na tabela OrderItems
            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "ProductName", "Quantity", "UnitPrice", "TotalPrice" },
                values: new object[,]
                {
                    { new Guid("11b23121-17ed-4b15-9dd2-2a9dc3cc288c"), new Guid("766aba8f-8c06-48a0-a939-41879f27be5a"), new Guid("19bb2513-fedf-44b6-c44c-08dd43d9cd34"), "Calabresa", 1, 24.00m, 24.00m },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new Guid("766aba8f-8c06-48a0-a939-41879f27be5a"), new Guid("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"), "Marguerita", 2, 26.00m, 52.00m },
                    { new Guid("d3158cfc-72e9-43a7-9b5e-34e2d9a55f61"), new Guid("3db07c1e-94b2-41fa-b9c3-8a8cc6a4fba1"), new Guid("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"), "Marguerita", 3, 26.00m, 78.00m },
                    { new Guid("3fa85f64-5717-4562-c3fc-3c963f66afa6"), new Guid("99b6bce5-9884-461b-9a45-8cfeb66b8804"), new Guid("f04acbb2-9c97-4fae-853b-e1c1e52715a2"), "Quatro queijos", 2, 40.00m, 80.00m },
                    { new Guid("c66cfce9-b460-4ad5-b7c3-f6f70370fb99"), new Guid("99b6bce5-9884-461b-9a45-8cfeb66b8804"), new Guid("19bb2513-fedf-44b6-c44c-08dd43d9cd34"), "Calabresa", 2, 24.00m, 48.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("11b23121-17ed-4b15-9dd2-2a9dc3cc288c"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("d3158cfc-72e9-43a7-9b5e-34e2d9a55f61"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("3fa85f64-5717-4562-c3fc-3c963f66afa6"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("c66cfce9-b460-4ad5-b7c3-f6f70370fb99"));

            // Remover dados da tabela Products
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("19bb2513-fedf-44b6-c44c-08dd43d9cd34"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f04acbb2-9c97-4fae-853b-e1c1e52715a2"));

            // Remover dados da tabela Orders
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("766aba8f-8c06-48a0-a939-41879f27be5a"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("3db07c1e-94b2-41fa-b9c3-8a8cc6a4fba1"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("99b6bce5-9884-461b-9a45-8cfeb66b8804"));

            // Remover dados da tabela Customers
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"));
        }
    }
}
