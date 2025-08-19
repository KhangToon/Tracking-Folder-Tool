using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Reflection;
using TrackingFolder.API.Contracts;
using TrackingFolder.API.Endpoints;
using TrackingFolder.API.Extensions;
using TrackingFolder.API.Helpers;
using TrackingFolder.API.Interfaces;

var localIP = PortFinder.GetLocalIPAddress();
IPAddress localIPAddress = IPAddress.Parse(localIP);
var port = PortFinder.FindAvailablePort_CheckUsed(localIPAddress);
var url = $"http://{localIP}:{port}";

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls(url);

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
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader());
});

var app = builder.Build();

// Enable Swagger UI in development
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Map API endpoints under /api/v1
//app.MapGroup("/api/v1")
//   .WithTags("Gold Expert Measure API")
//   .MapGoldExpertEndpoints();

app.MapGoldExpertEndpoints();

// In-memory data store
var dataStore = new List<Dictionary<string, string>>();

// POST endpoint
app.MapPost("/GEX8998", (List<Dictionary<string, string>> data) =>
{
    if (data == null || data.Count == 0)
    {
        return Results.BadRequest("Data is required.");
    }

    dataStore.AddRange(data);
    return Results.Created("/GEX8998", data);
});

// GET endpoint
app.MapGet("/GEX8998", () => Results.Ok(dataStore));

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();