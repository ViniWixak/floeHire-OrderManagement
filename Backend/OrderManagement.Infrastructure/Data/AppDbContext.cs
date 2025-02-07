using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using System;

namespace OrderManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar precisão e escala para as propriedades decimal
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.TotalPrice)
                .HasColumnType("decimal(18,2)");

            // Seeding Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = Guid.Parse("beb70355-350d-4c03-b3dd-608c2d7c6ecb"),
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Phone = "123-456-7890"
                }
            );

            // Seeding Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("19BB2513-FEDF-44B6-C44C-08DD43D9CD34"),
                    Name = "Calabresa",
                    Price = 24.00m
                },
                new Product
                {
                    Id = Guid.Parse("1A44F41F-B7D2-41C9-9497-0BC2C1A2613B"),
                    Name = "Marguerita",
                    Price = 26.00m
                },
                new Product
                {
                    Id = Guid.Parse("F04ACBB2-9C97-4FAE-853B-E1C1E52715A2"),
                    Name = "Quatro queijos",
                    Price = 40.00m
                }
            );

            // Seeding Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = Guid.Parse("766ABA8F-8C06-48A0-A939-41879F27BE5A"),
                    CustomerId = Guid.Parse("beb70355-350d-4c03-b3dd-608c2d7c6ecb"),
                    OrderDate = DateTime.Parse("2025-02-02 14:07:07.001"),
                    TotalAmount = 76.00m,
                    Status = OrderStatus.Pending
                },
                new Order
                {
                    Id = Guid.Parse("3DB07C1E-94B2-41FA-B9C3-8A8CC6A4FBA1"),
                    CustomerId = Guid.Parse("beb70355-350d-4c03-b3dd-608c2d7c6ecb"),
                    OrderDate = DateTime.Parse("2025-02-01 13:00:00"),
                    TotalAmount = 78.00m,
                    Status = OrderStatus.Pending
                },
                new Order
                {
                    Id = Guid.Parse("99B6BCE5-9884-461B-9A45-8CFEB66B8804"),
                    CustomerId = Guid.Parse("beb70355-350d-4c03-b3dd-608c2d7c6ecb"),
                    OrderDate = DateTime.Parse("2025-02-02 01:53:26.132"),
                    TotalAmount = 128.00m,
                    Status = OrderStatus.Pending
                }
            );

            // Seeding OrderItems
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    Id = Guid.Parse("11B23121-17ED-4B15-9DD2-2A9DC3CC288C"),
                    OrderId = Guid.Parse("766ABA8F-8C06-48A0-A939-41879F27BE5A"),
                    ProductId = Guid.Parse("19BB2513-FEDF-44B6-C44C-08DD43D9CD34"),
                    ProductName = "Calabresa",
                    Quantity = 1,
                    UnitPrice = 24.00m,
                    TotalPrice = 24.00m
                },
                new OrderItem
                {
                    Id = Guid.Parse("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                    OrderId = Guid.Parse("766ABA8F-8C06-48A0-A939-41879F27BE5A"),
                    ProductId = Guid.Parse("1A44F41F-B7D2-41C9-9497-0BC2C1A2613B"),
                    ProductName = "Marguerita",
                    Quantity = 2,
                    UnitPrice = 26.00m,
                    TotalPrice = 52.00m
                },
                new OrderItem
                {
                    Id = Guid.Parse("D3158CFC-72E9-43A7-9B5E-34E2D9A55F61"),
                    OrderId = Guid.Parse("3DB07C1E-94B2-41FA-B9C3-8A8CC6A4FBA1"),
                    ProductId = Guid.Parse("1A44F41F-B7D2-41C9-9497-0BC2C1A2613B"),
                    ProductName = "Marguerita",
                    Quantity = 3,
                    UnitPrice = 26.00m,
                    TotalPrice = 78.00m
                },
                new OrderItem
                {
                    Id = Guid.Parse("3FA85F64-5717-4562-C3FC-3C963F66AFA6"),
                    OrderId = Guid.Parse("99B6BCE5-9884-461B-9A45-8CFEB66B8804"),
                    ProductId = Guid.Parse("F04ACBB2-9C97-4FAE-853B-E1C1E52715A2"),
                    ProductName = "Quatro queijos",
                    Quantity = 2,
                    UnitPrice = 40.00m,
                    TotalPrice = 80.00m
                },
                new OrderItem
                {
                    Id = Guid.Parse("C66CFCE9-B460-4AD5-B7C3-F6F70370FB99"),
                    OrderId = Guid.Parse("99B6BCE5-9884-461B-9A45-8CFEB66B8804"),
                    ProductId = Guid.Parse("19BB2513-FEDF-44B6-C44C-08DD43D9CD34"),
                    ProductName = "Calabresa",
                    Quantity = 2,
                    UnitPrice = 24.00m,
                    TotalPrice = 48.00m
                }
            );
        }
    }
}
