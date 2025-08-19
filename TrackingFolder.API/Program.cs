using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using TrackingFolder.API.Contracts;
using TrackingFolder.API.Endpoints;
using TrackingFolder.API.Extensions;
using TrackingFolder.API.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Register application services and dependencies
builder.AddApplicationServices();

// Add controllers and minimal API explorer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger/OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Gold Expert Measure API", Version = "v1", Description = "Minimal API" });
    // Optionally include XML comments for better documentation
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // if (File.Exists(xmlPath))
    // {
    //     c.IncludeXmlComments(xmlPath);
    // }
});

// Enable CORS for frontend integration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Enable Swagger UI in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Map API endpoints under /api/v1
//app.MapGroup("/api/v1")
//   .WithTags("Gold Expert Measure API")
//   .MapGoldExpertEndpoints();

app.MapGoldExpertEndpoints();



app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();