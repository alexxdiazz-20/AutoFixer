using TallerAutomotriz.Application.Interfaces;
using TallerAutomotriz.Application.Services;
using TallerAutomotriz.Infrastructure;
using TallerAutomotriz.Infrastructure.Data;
using TallerAutomotriz.Infrastructure.Interfaces;
using TallerAutomotriz.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

// arreglo r



// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("TallerAutomotriz.Infrastructure")));

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register repositories and services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IMechanicService, MechanicService>();
builder.Services.AddScoped<IServiceOrderService, ServiceOrderService>();

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Taller Automotriz API",
        Version = "v1",
        Description = "API para la gestión de un taller automotriz",
        Contact = new OpenApiContact
        {
            Name = "Taller Automotriz",
            Email = "contact@tallerautomotriz.com",
            Url = new Uri("https://tallerautomotriz.com")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Taller Automotriz API v1"));
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers(); // Importante para los controladores API

// Create database if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        await DbSeeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.Run();