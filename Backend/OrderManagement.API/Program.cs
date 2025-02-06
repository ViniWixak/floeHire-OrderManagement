using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Handlers.Orders;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Data;
using OrderManagement.Infrastructure.Repositories;
using OrderManagement.Application.Handlers.Products;
using ProductManagement.Application.Handlers.Products;
using OrderManagement.Application.Handlers.OderItems;
using OrderManagement.Infrastructure.Configuration;
using MongoDB.Driver;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Interfaces.Mongo;
using OrderManagement.Domain.Models.MongoModel;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

        builder.Services.AddSingleton<MongoDbContext>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        var mongoSettings = builder.Configuration.GetSection("MongoSettings").Get<MongoDbSettings>();
        builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoSettings"));

        var mongoClient = new MongoClient(mongoSettings.ConnectionString);
        var database = mongoClient.GetDatabase("OrderDb");

        builder.Services.AddSingleton<IMongoClient>(mongoClient);
        builder.Services.AddSingleton(database);

        builder.Services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>(provider =>
        {
            var mongoClient = provider.GetRequiredService<IMongoClient>();
            var databaseName = provider.GetRequiredService<IConfiguration>().GetSection("MongoSettings").Get<MongoDbSettings>().DatabaseName;
            return new OrderReadRepository(mongoClient, databaseName);
        });

        builder.Services.AddSingleton<IOrderItemReadRepository, OrderItemReadRepository>();
        builder.Services.AddSingleton<IOrderItemWriteRepository, OrderItemWriteRepository>();

        builder.Services.AddScoped<IOrderSyncService, OrderSyncService>();


        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddTransient<IRequestHandler<GetOrderByIdQuery, Order>, GetOrderByIdQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetAllOrdersQuery, List<Order>>, GetAllOrdersQueryHandler>();
        builder.Services.AddScoped<IRequestHandler<CreateOrderCommand, Order>, CreateOrderCommandHandler>();
        builder.Services.AddScoped<IRequestHandler<DeleteOrderByIdCommand, Order>, DeleteOrderByIdCommandHandler>();
        builder.Services.AddScoped<IRequestHandler<UpdateOrderByIdCommand, Order>, UpdateOrderByIdCommandHandler>();

        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddTransient<IRequestHandler<GetAllProductsQuery, List<Product>>, GetAllProductsQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetProductByIdQuery, Product>, GetProductByIdQueryHandler>();
        builder.Services.AddScoped<IRequestHandler<UpdateProductByIdCommand, Product>, UpdateProductByIdCommandHandler>();
        builder.Services.AddScoped<IRequestHandler<CreateProductCommand, Product>, CreateProductCommandHandler>();
        builder.Services.AddScoped<IRequestHandler<DeleteProductByIdCommand, Product>, DeleteProductByIdCommandHandler>();

        builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        builder.Services.AddTransient<IRequestHandler<GetOrderItemsQuery, IEnumerable<OrderItem>>, GetOrderItemsQueryHandler>();
        builder.Services.AddScoped<IRequestHandler<CreateOrderItemCommand, OrderItem>, CreateOrderItemCommandHandler>();
        builder.Services.AddScoped<IRequestHandler<DeleteOrderItemCommand, OrderItem>, DeleteOrderItemCommandHandler>();

        builder.Services.AddTransient<IRequestHandler<GetAllOrdersFromMongoQuery, IEnumerable<OrderMongoModel>>, GetAllOrdersFromMongoQueryHandler>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });


        var app = builder.Build();

        app.UseCors("AllowAll");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}