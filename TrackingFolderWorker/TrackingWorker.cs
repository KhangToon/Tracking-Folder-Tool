using ConsoleTables;
using System.Net.Http.Json;
using System.Text.Json;
using TrackingFolderWorker.Logs;
using TrackingFolderWorker.Models;
using TrackingFolderWorker.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrackingFolderWorker
{
    public class TrackingWorker : BackgroundService
    {
        private readonly ILogger<TrackingWorker> _logger;
        private readonly HttpClient _httpClient;
        private readonly FileSystemWatcher _watcher;
        private readonly IConfiguration _configuration;
        private readonly string? _folderPath;
        private readonly string? _hostAPIurl;
        private readonly string? _goldExId;
        private readonly List<string>? _targetHeaders;
        private readonly int? _getrows;

        public IEnumerable<FileDetail> FileLists { get; private set; }

        // Fix for CS8618: Initialize _watcher and FileLists in the constructor

        public TrackingWorker(ILogger<TrackingWorker> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;

            _goldExId = _configuration["GoldExpertID"];
            _hostAPIurl = _configuration["Host_API"];
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri($"{_hostAPIurl}"); // API Backend URL
            _watcher = new FileSystemWatcher(); // Initialize _watcher
            FileLists = []; // Initialize FileLists

            // Get tracking folder path from configuration or environment variable
            _folderPath = _configuration["TrackingFolderPath"];
            _targetHeaders = _configuration["Headers"]?.Split(',').Select(h => h.Trim()).ToList();
            _getrows = int.TryParse(_configuration["GetRow"]?.ToString(), out int r) ? r : null;

            _logger.LogInformation("Remote API host: " + _httpClient.BaseAddress + $"/{_goldExId}");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                if (string.IsNullOrEmpty(_folderPath) || !Directory.Exists(_folderPath))
                {
                    _logger.LogError("Invalid or missing TrackingFolderPath in appsettings.json.");
                    return;
                }

                // Start watching the specified folder
                StartWatching(_folderPath);

                while (!stoppingToken.IsCancellationRequested)
                {
                    //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                    await Task.Delay(1000, stoppingToken); // keep service alive
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the worker.");
                //_logger.LogInformation(ex.Message);
                Environment.Exit(1); // Exit with non-zero code for Windows Service recovery
            }
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
            _watcher.Filter = "*.*"; // All files
            _watcher.Created += OnCreated;
            //_watcher.Changed += OnChanged;
            _watcher.IncludeSubdirectories = true; // Optional: monitor subdirectories too
            _watcher.EnableRaisingEvents = true;

            _logger.LogInformation("FolderWatcherService started and watching: " + _watcher.Path);
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

                // Only tracking .csv file
                if (fileDetail.Extension != ".csv") return;

                // Log file details to console
                ConsoleLog.Log("File Details:");
                ConsoleLog.Log($"  Name: {fileDetail.Name}");
                ConsoleLog.Log($"  Extension: {fileDetail.Extension}");
                ConsoleLog.Log($"  Full Path: {fileDetail.FullPath}");
                ConsoleLog.Log($"  Size: {fileDetail.Size} bytes");
                ConsoleLog.Log($"  Created: {fileDetail.Created}");
                ConsoleLog.Log($"  Action: {fileDetail.ActionType}");

                var isAlreadyExist = FileLists.Where(f => f.Name == fileInfo.Name && f.Extension == fileInfo.Extension).Any();

                if (!isAlreadyExist)
                {
                    //string json = JsonSerializer.Serialize(fileDetail);

                    // Extract data from file 
                    var (Headers, Data) = CsvReaderService.CsvReader.ReadCsvFileDynamic(fileDetail.FullPath, _targetHeaders);

                    // Log data table to console
                    // ConsoleLog.LogData(Headers, Data, null, _getrows);

                    // Serialization Data
                    //var jsonData = JsonSerializer.Serialize(Data);
                    //_logger.LogInformation("Sending data: " + jsonData);

                    // Send data to API
                    var response = await _httpClient.PostAsJsonAsync($"/{_goldExId}", Data);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError($"Failed to push data: {response.StatusCode}");
                    }
                    else
                    {
                        _logger.LogInformation("Data pushed successfully." + DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending notification: {ex.Message}");
            }
        }


    }
}
