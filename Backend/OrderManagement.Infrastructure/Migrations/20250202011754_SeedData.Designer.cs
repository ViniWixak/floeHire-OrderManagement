﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderManagement.Infrastructure.Data;

#nullable disable

namespace OrderManagement.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250202011754_SeedData")]
    partial class SeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrderManagement.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"),
                            Email = "john.doe@example.com",
                            Name = "John Doe",
                            Phone = "123-456-7890"
                        },
                        new
                        {
                            Id = new Guid("ee4e3b6e-123d-4ef0-b44d-3cb2217bb48a"),
                            Email = "jane.smith@example.com",
                            Name = "Jane Smith",
                            Phone = "098-765-4321"
                        });
                });

            modelBuilder.Entity("OrderManagement.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b3475369-74bb-4f50-b17e-d50a3a3c09c7"),
                            CustomerId = new Guid("beb70355-350d-4c03-b3dd-608c2d7c6ecb"),
                            OrderDate = new DateTime(2025, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            TotalAmount = 250.00m
                        },
                        new
                        {
                            Id = new Guid("3db07c1e-94b2-41fa-b9c3-8a8cc6a4fba1"),
                            CustomerId = new Guid("ee4e3b6e-123d-4ef0-b44d-3cb2217bb48a"),
                            OrderDate = new DateTime(2025, 2, 1, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            TotalAmount = 450.00m
                        });
                });

            modelBuilder.Entity("OrderManagement.Domain.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8979bfc9-7746-42c5-ae2f-d1e349dd96ff"),
                            OrderId = new Guid("b3475369-74bb-4f50-b17e-d50a3a3c09c7"),
                            ProductId = new Guid("f04acbb2-9c97-4fae-853b-e1c1e52715a2"),
                            ProductName = "Product A",
                            Quantity = 2,
                            UnitPrice = 100.00m
                        },
                        new
                        {
                            Id = new Guid("d3158cfc-72e9-43a7-9b5e-34e2d9a55f61"),
                            OrderId = new Guid("3db07c1e-94b2-41fa-b9c3-8a8cc6a4fba1"),
                            ProductId = new Guid("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"),
                            ProductName = "Product B",
                            Quantity = 3,
                            UnitPrice = 150.00m
                        });
                });

            modelBuilder.Entity("OrderManagement.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f04acbb2-9c97-4fae-853b-e1c1e52715a2"),
                            Name = "Product A",
                            Price = 100.00m
                        },
                        new
                        {
                            Id = new Guid("1a44f41f-b7d2-41c9-9497-0bc2c1a2613b"),
                            Name = "Product B",
                            Price = 150.00m
                        });
                });

            modelBuilder.Entity("OrderManagement.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("OrderManagement.Domain.Entities.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrderManagement.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
