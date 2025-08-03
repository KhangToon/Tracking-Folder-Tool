using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Tracking_Folder_Tool.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddSingleton<FolderWatcherService>(); // <-- Add this line
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


var app = builder.Build();

// Force FolderWatcherService to start
app.Services.GetRequiredService<FolderWatcherService>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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


app.Run();
