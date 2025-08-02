using BackendLab.Api.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var students = new List<Student>();

students.Add(new Student(1, "gaelle", "gaelle@gmail.com"));
students.Add(new Student(2, "chloe", "chloe@gmail.com"));
students.Add(new Student(3, "chris", "chris@gmail.com"));
students.Add(new Student(4, "John", "John@gmail.com"));


app.Run();

