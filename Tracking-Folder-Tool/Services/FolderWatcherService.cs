using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Tracking_Folder_Tool.Data;

public class FolderWatcherService : IDisposable
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly FileSystemWatcher _watcher;

    public FolderWatcherService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
        _watcher = new FileSystemWatcher();
        StartWatching();
    }

    private void StartWatching()
    {
        string path = @"D:\KHANG DATA\PROJECT\TRACKING FOLDER\FolderToTracking";

        // Verify directory exists
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        _watcher.Path = path;
        _watcher.Filter = "*.*";
        _watcher.Created += OnCreated;
        _watcher.IncludeSubdirectories = true; // Optional: monitor subdirectories too
        _watcher.EnableRaisingEvents = true;

        Console.WriteLine("FolderWatcherService started and watching: " + _watcher.Path);
    }

    private async void OnCreated(object sender, FileSystemEventArgs e)
    {
        try
        {
            var fileInfo = new FileInfo(e.FullPath);
            var fileDetail = new
            {
                Name = fileInfo.Name,
                FullPath = fileInfo.FullName,
                Size = fileInfo.Exists ? fileInfo.Length : 0,
                Created = fileInfo.Exists ? fileInfo.CreationTime : DateTime.MinValue
            };
            string json = JsonSerializer.Serialize(fileDetail);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending notification: {ex.Message}");
        }
    }

    public void Dispose()
    {
        _watcher?.Dispose();
    }
}