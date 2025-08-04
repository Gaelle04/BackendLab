using BackendLab.Api.Models;
using BackendLab.Api.Controllers;
using BackendLab.Api.Services; 
using BackendLab.Api.Middleware;
using BackendLab.Api.Filters;
using MediatR;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<HeaderFilter>();
});
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddDbContext<BackendLabDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(9, 3, 0))));

builder.Services.AddDbContext<BackendLabDbContextCodeFirst>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<HelloMiddleware>();
app.MapControllers();


app.Run();

