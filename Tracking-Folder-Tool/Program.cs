using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using System.Diagnostics;
using System.Net;
using Tracking_Folder_Tool;
using Tracking_Folder_Tool.Data;
using Tracking_Folder_Tool.Services;

// Find an available port
var localIP = PortFinder.GetLocalIPAddress();
IPAddress localIPAddress = IPAddress.Parse(localIP);
var port = PortFinder.FindAvailablePort_CheckUsed(localIPAddress);
var url = $"http://{localIP}:{port}";

var builder = WebApplication.CreateBuilder(args);

// Add Radzen components
builder.Services.AddRadzenComponents();
// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddSingleton<FolderWatcherService>(); // <-- Add this line
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddSingleton<DirectoryService>();

var app = builder.Build();

// Force FolderWatcherService to start
app.Services.GetRequiredService<FolderWatcherService>();

// Configure the HTTP request pipeline.

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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapHub<NotificationHub>("/notificationHub");

// Open browser after application starts
app.Urls.Add(url);
app.Lifetime.ApplicationStarted.Register(() =>
{
    var psi = new ProcessStartInfo
    {
        FileName = url,
        UseShellExecute = true
    };
    Process.Start(psi);
});
///////////

app.Run();
