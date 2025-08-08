using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Tracking_Folder_Tool.Data;
using Tracking_Folder_Tool.Commons;

public class FolderWatcherService : IDisposable
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly FileSystemWatcher _watcher;

    public FolderWatcherService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
        _watcher = new FileSystemWatcher();
    }

    public string GetWatcherPath()
    {
        return _watcher.Path;
    }

    public void SetWatcherPath(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }

        if (string.IsNullOrEmpty(_watcher.Path))
        {
            StartWatching(path);
        }

        _watcher.Path = path;
    }

    private void StartWatching(string path)
    {
        if (string.IsNullOrEmpty(path)) { return; }

        // Verify directory exists
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        _watcher.Path = path;
        _watcher.Filter = "*.*";
        _watcher.Created += OnCreated;
        _watcher.Changed += OnChanged;
        _watcher.IncludeSubdirectories = true; // Optional: monitor subdirectories too
        _watcher.EnableRaisingEvents = true;

        Console.WriteLine("FolderWatcherService started and watching: " + _watcher.Path);
    }

    private async void OnChanged(object sender, FileSystemEventArgs e)
    {
        try
        {
            var fileInfo = new FileInfo(e.FullPath);
            var fileDetail = new
            {
                Extension = fileInfo.Extension,
                Name = fileInfo.Name,
                FullPath = fileInfo.FullName,
                Size = fileInfo.Exists ? fileInfo.Length : 0,
                Changed = fileInfo.Exists ? fileInfo.LastWriteTime : DateTime.MinValue,
                ActionType = "File Changed"
            };

            var isAlreadyExist = Common.FileLists.Where(f => f.Name == fileInfo.Name && f.Extension == fileInfo.Extension).Any();

            if (isAlreadyExist)
            {
                string json = JsonSerializer.Serialize(fileDetail);
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", json);
            }
            else
            {
                HandleWithFileChanged();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending notification: {ex.Message}");
        }
    }

    private void HandleWithFileChanged()
    {

    }

    private async void OnCreated(object sender, FileSystemEventArgs e)
    {
        try
        {
            var fileInfo = new FileInfo(e.FullPath);
            var fileDetail = new
            {
                Extension = fileInfo.Extension,
                Name = fileInfo.Name,
                FullPath = fileInfo.FullName,
                Size = fileInfo.Exists ? fileInfo.Length : 0,
                Created = fileInfo.Exists ? fileInfo.CreationTime : DateTime.MinValue,
                ActionType = "File Created"
            };

            var isAlreadyExist = Common.FileLists.Where(f => f.Name == fileInfo.Name && f.Extension == fileInfo.Extension).Any();

            if (!isAlreadyExist)
            {
                string json = JsonSerializer.Serialize(fileDetail);
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", json);
            }
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