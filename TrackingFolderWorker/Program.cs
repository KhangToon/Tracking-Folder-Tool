using TrackingFolderWorker;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<TrackingWorker>();
        services.AddHttpClient();
        // Đăng ký IConfiguration để inject vào Worker
        services.AddSingleton(hostContext.Configuration);
    })
    .ConfigureAppConfiguration((context, config) =>
    {
        // Thêm appsettings.json vào configuration
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .Build();

await host.RunAsync();
