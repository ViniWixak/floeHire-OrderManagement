using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;

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
        //seeding data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = Guid.Parse("beb70355-350d-4c03-b3dd-608c2d7c6ecb"),
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Phone = "123-456-7890"
                },
                new Customer
                {
                    Id = Guid.Parse("ee4e3b6e-123d-4ef0-b44d-3cb2217bb48a"),
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Phone = "098-765-4321"
                }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("f04acbb2-9c97-4fae-853b-e1c1e52715a2"),
                    Name = "Product A",
                    Price = 100.00m
                },
                new Product
                {
                    Id = Guid.Parse("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"),
                    Name = "Product B",
                    Price = 150.00m
                }
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = Guid.Parse("b3475369-74bb-4f50-b17e-d50a3a3c09c7"),
                    CustomerId = Guid.Parse("beb70355-350d-4c03-b3dd-608c2d7c6ecb"),
                    OrderDate = DateTime.Parse("2025-02-01T12:00:00"),
                    TotalAmount = 250.00m,
                    Status = OrderStatus.Pending
                },
                new Order
                {
                    Id = Guid.Parse("3db07c1e-94b2-41fa-b9c3-8a8cc6a4fba1"),
                    CustomerId = Guid.Parse("ee4e3b6e-123d-4ef0-b44d-3cb2217bb48a"),
                    OrderDate = DateTime.Parse("2025-02-01T13:00:00"),
                    TotalAmount = 450.00m,
                    Status = OrderStatus.Pending
                }
            );

            // Seed OrderItems
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    Id = Guid.Parse("8979bfc9-7746-42c5-ae2f-d1e349dd96ff"),
                    OrderId = Guid.Parse("b3475369-74bb-4f50-b17e-d50a3a3c09c7"),
                    ProductId = Guid.Parse("f04acbb2-9c97-4fae-853b-e1c1e52715a2"),
                    ProductName = "Product A",
                    Quantity = 2,
                    UnitPrice = 100.00m
                },
                new OrderItem
                {
                    Id = Guid.Parse("d3158cfc-72e9-43a7-9b5e-34e2d9a55f61"),
                    OrderId = Guid.Parse("3db07c1e-94b2-41fa-b9c3-8a8cc6a4fba1"),
                    ProductId = Guid.Parse("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"),
                    ProductName = "Product B",
                    Quantity = 3,
                    UnitPrice = 150.00m
                }
            );
        }
    }
}

