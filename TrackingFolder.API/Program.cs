using System.Reflection;
using TrackingFolder.API.Endpoints;
using TrackingFolder.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

////
// Register application services
builder.AddApplicationServices();   

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

////
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Gold Expert Measure API", Version = "v1", Description = "Minimal API" });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Enable CORS (enable for call api from frontend)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

////
app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

////
// Map API endpoints
app.MapGroup("/api/v1/")
   .WithTags("Gold Expert Measure API")
   .MapGoldExpertEndpoints();

app.Run();
