﻿using AmazingJourney.Application.Services;
using AmazingJourney.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using AmazingJourney.Application.Mappings;
using AutoMapper;
using AmazingJourney.Application.Interfaces;


var builder = WebApplication.CreateBuilder(args);


// Đăng ký ICategoryService
builder.Services.AddMvc();
// Đăng ký AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IHotelService, HotelService>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("AmazingJourney.Infrastructure"))
);
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