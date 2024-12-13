using Microsoft.EntityFrameworkCore;
using rabbitMQ.Data;
using rabbitMQ.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=postgres;Port=5432;Database=testdb;Username=user;Password=password"));

builder.Services.AddSingleton<RabbitMqService>();  // Register RabbitMqService

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
app.MapControllers();

app.Run();