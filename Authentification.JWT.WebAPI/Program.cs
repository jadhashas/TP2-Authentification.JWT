using System;
using Authentification.JWT.DAL.Context;
using Authentification.JWT.Service.Services;
using Authentification.JWT.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Charger la chaîne de connexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Ajouter DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enregistrement d’AutoMapper
builder.Services.AddAutoMapper(typeof(Authentification.JWT.Service.Mapping.MappingProfile).Assembly);
// Enregistrement du service
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


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
