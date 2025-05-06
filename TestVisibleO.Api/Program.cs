using Application.Application.Interfaces;
using Application.Application.Services;
using Microsoft.EntityFrameworkCore;
using System;
using TestVisibleO.Domain.Interfaces;
using TestVisibleO.Infrastructure.Data;
using TestVisibleO.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region DB CONTEXT CONFIG
builder.Services.AddDbContext<TestProductsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection") ?? throw new ArgumentNullException());
    options.EnableSensitiveDataLogging();
});
#endregion


// Inyección de dependencias
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
