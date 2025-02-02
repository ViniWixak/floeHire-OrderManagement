using System;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Handlers.Orders;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Data;
using OrderManagement.Infrastructure.Repositories;
using OrderManagement.Application.Handlers.Products;
using ProductManagement.Application.Handlers.Products;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
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
