using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Name", "Email", "Phone" },
                values: new object[] {
                Guid.Parse("BEB70355-350D-4C03-B3DD-608C2D7C6ECB"),
                "John Doe",
                "john.doe@example.com",
                "123-456-7890"
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,] {
                { Guid.Parse("19BB2513-FEDF-44B6-C44C-08DD43D9CD34"), "Xbox", 5100.00 },
                { Guid.Parse("1A44F41F-B7D2-41C9-9497-0BC2C1A2613B"), "Iphone", 9800.00 },
                { Guid.Parse("F04ACBB2-9C97-4FAE-853B-E1C1E52715A2"), "Notebook", 4250.00 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "TotalAmount", "Status" },
                values: new object[,] {
                { Guid.Parse("766ABA8F-8C06-48A0-A939-41879F27BE5A"), Guid.Parse("BEB70355-350D-4C03-B3DD-608C2D7C6ECB"), new DateTime(2025, 2, 2, 14, 7, 7, 1), 550.00, 0 },
                { Guid.Parse("3DB07C1E-94B2-41FA-B9C3-8A8CC6A4FBA1"), Guid.Parse("BEB70355-350D-4C03-B3DD-608C2D7C6ECB"), new DateTime(2025, 2, 1, 13, 0, 0), 450.00, 0 },
                { Guid.Parse("99B6BCE5-9884-461B-9A45-8CFEB66B8804"), Guid.Parse("BEB70355-350D-4C03-B3DD-608C2D7C6ECB"), new DateTime(2025, 2, 2, 1, 53, 26, 132), 0.00, 0 }
                });


            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "ProductName", "Quantity", "UnitPrice", "TotalPrice" },
                values: new object[,] {
                { Guid.Parse("11B23121-17ED-4B15-9DD2-2A9DC3CC288C"), Guid.Parse("766ABA8F-8C06-48A0-A939-41879F27BE5A"), Guid.Parse("19BB2513-FEDF-44B6-C44C-08DD43D9CD34"), "Xbox", 1, 5100.00, 5100.00 },
                { Guid.Parse("3FA85F64-5717-4562-B3FC-2C963F66AFA6"), Guid.Parse("766ABA8F-8C06-48A0-A939-41879F27BE5A"), Guid.Parse("1A44F41F-B7D2-41C9-9497-0BC2C1A2613B"), "Iphone", 2, 9800.00, 19600.00 },
                { Guid.Parse("D3158CFC-72E9-43A7-9B5E-34E2D9A55F61"), Guid.Parse("3DB07C1E-94B2-41FA-B9C3-8A8CC6A4FBA1"), Guid.Parse("1A44F41F-B7D2-41C9-9497-0BC2C1A2613B"), "Iphone", 3, 9800.00, 29400.00 },
                { Guid.Parse("3FA85F64-5717-4562-C3FC-3C963F66AFA6"), Guid.Parse("99B6BCE5-9884-461B-9A45-8CFEB66B8804"), Guid.Parse("F04ACBB2-9C97-4FAE-853B-E1C1E52715A2"), "Noteebook", 2, 4250.00, 8500.00 },
                { Guid.Parse("C66CFCE9-B460-4AD5-B7C3-F6F70370FB99"), Guid.Parse("99B6BCE5-9884-461B-9A45-8CFEB66B8804"), Guid.Parse("19BB2513-FEDF-44B6-C44C-08DD43D9CD34"), "Xbox", 2, 5100.00, 10200.00 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValues: new object[] {
                Guid.Parse("11B23121-17ED-4B15-9DD2-2A9DC3CC288C"),
                Guid.Parse("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                Guid.Parse("D3158CFC-72E9-43A7-9B5E-34E2D9A55F61"),
                Guid.Parse("3FA85F64-5717-4562-C3FC-3C963F66AFA6"),
                Guid.Parse("C66CFCE9-B460-4AD5-B7C3-F6F70370FB99")
                });
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValues: new object[] {
                Guid.Parse("766ABA8F-8C06-48A0-A939-41879F27BE5A"),
                Guid.Parse("3DB07C1E-94B2-41FA-B9C3-8A8CC6A4FBA1"),
                Guid.Parse("99B6BCE5-9884-461B-9A45-8CFEB66B8804")
                });
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValues: new object[] {
                Guid.Parse("19BB2513-FEDF-44B6-C44C-08DD43D9CD34"),
                Guid.Parse("1A44F41F-B7D2-41C9-9497-0BC2C1A2613B"),
                Guid.Parse("F04ACBB2-9C97-4FAE-853B-E1C1E52715A2")
                });
            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValues: new object[] {
                Guid.Parse("BEB70355-350D-4C03-B3DD-608C2D7C6ECB")
                });
        }

    }
}
