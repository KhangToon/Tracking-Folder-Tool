using ConsoleTables;
using System.Net.Http.Json;
using System.Text.Json;
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

        public IEnumerable<FileDetail> FileLists { get; private set; }

        // Fix for CS8618: Initialize _watcher and FileLists in the constructor

        public TrackingWorker(ILogger<TrackingWorker> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/"); // API Backend URL
            _watcher = new FileSystemWatcher(); // Initialize _watcher
            FileLists = []; // Initialize FileLists
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Get tracking folder path from configuration or environment variable
            string? folderPath = _configuration["TrackingFolderPath"];

            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                _logger.LogError("Invalid or missing TrackingFolderPath in appsettings.json.");
                return;
            }

            // Start watching the specified folder
            StartWatching(folderPath);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken); // keep service alive
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

                var isAlreadyExist = FileLists.Where(f => f.Name == fileInfo.Name && f.Extension == fileInfo.Extension).Any();

                if (isAlreadyExist)
                {
                    // Only tracking csv files
                    //if (fileDetail.Extension != ".csv") return;

                    //string json = JsonSerializer.Serialize(fileDetail);
                    // Extract data from file 
                    var (Headers, Data) = CsvReaderService.CsvReader.ReadCsvFileDynamic(fileDetail.FullPath);

                    var response = await _httpClient.PostAsJsonAsync("data", Data);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError($"Failed to push data: {response.StatusCode}");
                        Console.WriteLine($"Failed to push data: {response.StatusCode}");
                    }
                    else
                    {
                        _logger.LogInformation("Data pushed successfully." + DateTime.Now);
                        Console.WriteLine("Data pushed successfully." + DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                Console.WriteLine($"Error sending notification: {ex.Message}");
            }
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

                // Log file details to console
                Console.WriteLine("File Details:");
                Console.WriteLine($"  Name: {fileDetail.Name}");
                Console.WriteLine($"  Extension: {fileDetail.Extension}");
                Console.WriteLine($"  Full Path: {fileDetail.FullPath}");
                Console.WriteLine($"  Size: {fileDetail.Size} bytes");
                Console.WriteLine($"  Created: {fileDetail.Created}");
                Console.WriteLine($"  Action: {fileDetail.ActionType}");

                var isAlreadyExist = FileLists.Where(f => f.Name == fileInfo.Name && f.Extension == fileInfo.Extension).Any();

                if (!isAlreadyExist)
                {
                    //string json = JsonSerializer.Serialize(fileDetail);
                    // Only tracking csv files
                    //if (fileDetail.Extension != ".csv") return;

                    //string json = JsonSerializer.Serialize(fileDetail);
                    // Extract data from file 
                    var (Headers, Data) = CsvReaderService.CsvReader.ReadCsvFileDynamic(fileDetail.FullPath);

                    // Take only the first 10 headers
                    var selectedHeaders = Headers.Take(10).ToList();

                    // Display CSV data as a table with up to 10 columns
                    Console.WriteLine("\nCSV Data Table (First 10 Columns):");
                    var table = new ConsoleTable(selectedHeaders.ToArray());

                    // Add each row to the table, only including values for selected headers
                    foreach (var record in Data)
                    {
                        var values = selectedHeaders.Select(header => record.TryGetValue(header, out var value) ? value : string.Empty).ToArray();
                        table.AddRow(values);
                    }

                    // Write the table to console
                    table.Write(Format.Alternative);
                    // Send data to API
                    var response = await _httpClient.PostAsJsonAsync("data", Data);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError($"Failed to push data: {response.StatusCode}");
                        Console.WriteLine($"Failed to push data: {response.StatusCode}");
                    }
                    else
                    {
                        _logger.LogInformation("Data pushed successfully." + DateTime.Now);
                        Console.WriteLine("Data pushed successfully." + DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending notification: {ex.Message}");
            }
        }
    }
}
