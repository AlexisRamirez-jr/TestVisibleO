using Microsoft.EntityFrameworkCore;
using TestVisibleO.Application.Interfaces;
using TestVisibleO.Application.Services;
using TestVisibleO.Domain.Interfaces;
using TestVisibleO.Infrastructure.Data;
using TestVisibleO.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Product/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
