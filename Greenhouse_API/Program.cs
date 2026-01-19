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

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<GreenHouseDbContext>(options =>
{
    var cs = builder.Configuration["ConnectionStrings:DefaultConnection"];

    if (string.IsNullOrWhiteSpace(cs))
        throw new Exception("Connection string is NULL or EMPTY");

    options.UseNpgsql(cs);
});
Console.WriteLine("CS = " + builder.Configuration["ConnectionStrings:DefaultConnection"]);

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

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

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
