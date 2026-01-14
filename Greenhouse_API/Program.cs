using Greenhouse_API.Data;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Extensions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Greenhouse_API.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<GreenHouseDbContext>(options =>
//options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDbContext<GreenHouseDbContext>(options =>
   options.UseNpgsql(
        builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Greenhouse API",
        Version = "v1",
        Description = "API to manage a smart green house"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features
            .Get<IExceptionHandlerFeature>()?.Error;

        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case NotFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new { error = exception.Message });
                break;

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = "Server error" });
                break;
        }
    });
});

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
