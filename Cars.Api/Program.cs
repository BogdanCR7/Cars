using Cars.Api;
using Context;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Services;
using System;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(db => db.UseSqlServer(connectionString),
    ServiceLifetime.Singleton);
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AllowNullCollections = true;
});


// Register scoped services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

// Register data providers
builder.Services.AddScoped<IEmployeeDataProvider, EmployeeDataProvider>();
builder.Services.AddScoped<IVehicleDataProvider, VehicleDataProvider>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
