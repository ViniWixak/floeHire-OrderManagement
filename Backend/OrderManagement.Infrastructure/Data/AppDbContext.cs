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
                .Property(o => o.TotalPrice)
                .HasColumnName("TotalPrice")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

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
                    Id = Guid.Parse("e5475b04-4b6a-4f0f-ab6d-ae3a274ff9fc"),
                    CustomerId = Guid.Parse("beb70355-350d-4c03-b3dd-608c2d7c6ecb"),
                    OrderDate = DateTime.Parse("2025-02-07 15:38:23"),
                    TotalAmount = 24.00m,
                    Status = OrderStatus.Pending
                },
                new Order
                {
                    Id = Guid.Parse("109909ef-a52c-4aa7-a9f5-9cd0df8f48e7"),
                    CustomerId = Guid.Parse("beb70355-350d-4c03-b3dd-608c2d7c6ecb"),
                    OrderDate = DateTime.Parse("2025-02-07 15:38:23"),
                    TotalAmount = 26.00m,
                    Status = OrderStatus.Shipped
                },
                new Order
                {
                    Id = Guid.Parse("1cb90969-73e3-4d34-94a2-48acc8300062"),
                    CustomerId = Guid.Parse("beb70355-350d-4c03-b3dd-608c2d7c6ecb"),
                    OrderDate = DateTime.Parse("2025-02-07 15:38:23"),
                    TotalAmount = 80.00m,
                    Status = OrderStatus.Canceled
                }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    Id = Guid.Parse("11B23121-17ED-4B15-9DD2-2A9DC3CC288C"),
                    OrderId = Guid.Parse("e5475b04-4b6a-4f0f-ab6d-ae3a274ff9fc"), 
                    ProductId = Guid.Parse("19BB2513-FEDF-44B6-C44C-08DD43D9CD34"),
                    ProductName = "Calabresa",
                    Quantity = 1,
                    UnitPrice = 24.00m,
                    TotalPrice = 24.00m
                },
                new OrderItem
                {
                    Id = Guid.Parse("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                    OrderId = Guid.Parse("109909ef-a52c-4aa7-a9f5-9cd0df8f48e7"), 
                    ProductId = Guid.Parse("1A44F41F-B7D2-41C9-9497-0BC2C1A2613B"),
                    ProductName = "Marguerita",
                    Quantity = 1,
                    UnitPrice = 26.00m,
                    TotalPrice = 26.00m
                },
                new OrderItem
                {
                    Id = Guid.Parse("D3158CFC-72E9-43A7-9B5E-34E2D9A55F61"),
                    OrderId = Guid.Parse("1cb90969-73e3-4d34-94a2-48acc8300062"), 
                    ProductId = Guid.Parse("f04acbb2-9c97-4fae-853b-e1c1e52715a2"),
                    ProductName = "Quatro queijos",
                    Quantity = 2,
                    UnitPrice = 40.00m,
                    TotalPrice = 80.00m
                }
            );
        }
    }
}
