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
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

var mongoSettings = builder.Configuration.GetSection("MongoSettings").Get<MongoDbSettings>();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoSettings"));

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoSettings.ConnectionString));
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(mongoSettings.DatabaseName);
});

builder.Services.AddScoped<IMongoCollection<OrderMongoModel>>(sp =>
{
    var database = sp.GetRequiredService<IMongoDatabase>();
    return database.GetCollection<OrderMongoModel>(mongoSettings.OrdersCollection);
});

builder.Services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
//builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();

builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>(provider =>
{
    var mongoClient = provider.GetRequiredService<IMongoClient>();
    var databaseName = provider.GetRequiredService<IConfiguration>().GetSection("MongoSettings").Get<MongoDbSettings>().DatabaseName;
    return new OrderReadRepository(mongoClient, databaseName);
});

builder.Services.AddScoped<IOrderItemReadRepository, OrderItemReadRepository>();
builder.Services.AddScoped<IOrderItemWriteRepository, OrderItemWriteRepository>();

builder.Services.AddScoped<IOrderSyncService, OrderSyncService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

builder.Services.AddScoped<MongoDbContext>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddTransient<IRequestHandler<GetOrderByIdQuery, Order>, GetOrderByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllOrdersQuery, List<Order>>, GetAllOrdersQueryHandler>();
builder.Services.AddScoped<IRequestHandler<CreateOrderCommand, Order>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteOrderByIdCommand, Order>, DeleteOrderByIdCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateOrderByIdCommand, Order>, UpdateOrderByIdCommandHandler>();

builder.Services.AddTransient<IRequestHandler<GetAllProductsQuery, List<Product>>, GetAllProductsQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetProductByIdQuery, Product>, GetProductByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateProductByIdCommand, Product>, UpdateProductByIdCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CreateProductCommand, Product>, CreateProductCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteProductByIdCommand, Product>, DeleteProductByIdCommandHandler>();

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();  // Aplica as migrations pendentes
}

using (var scope = app.Services.CreateScope())
{
    var dataSeeder = scope.ServiceProvider.GetRequiredService<IOrderWriteRepository>();
    await dataSeeder.SeedDataAsync();  // Seeder de dados no MongoDB
}

app.Run();
