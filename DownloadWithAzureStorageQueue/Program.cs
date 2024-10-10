using Azure.Storage.Queues; // Install Azure.Storage.Queues package
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DownloadWithAzureStorageQueue;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();

    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService(sp => 
                    new UrlDownloaderWorker("your_connection_string", "urlqueue", sp.GetRequiredService<ILogger<UrlDownloaderWorker>>()));
            });

        return hostBuilder;
    }
}


public class UrlEnqueuer
{
    private readonly QueueClient _queueClient;

    public UrlEnqueuer(string connectionString, string queueName)
    {
        _queueClient = new QueueClient(connectionString, queueName);
        _queueClient.CreateIfNotExists();
    }

    public async Task EnqueueUrlsAsync(IEnumerable<string> urls)
    {
        foreach (var url in urls)
        {
            await _queueClient.SendMessageAsync(Convert.ToBase64String(Encoding.UTF8.GetBytes(url)));
        }
    }

    private async Task ReadUrlsFromFileAndEnqueue()
    {

        // Usage
        var enqueuer = new UrlEnqueuer("your_connection_string", "urlqueue");
        await enqueuer.EnqueueUrlsAsync(File.ReadLines("urls.txt"));
    }
}


public class UrlDownloaderWorker : BackgroundService 
{
    private readonly QueueClient _queueClient;
    private readonly ILogger<UrlDownloaderWorker> _logger;

    public UrlDownloaderWorker(string connectionString, string queueName, ILogger<UrlDownloaderWorker> logger)
    {
        _queueClient = new QueueClient(connectionString, queueName);
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            QueueMessage[] messages = await _queueClient.ReceiveMessagesAsync(maxMessages: 10);

            foreach (QueueMessage message in messages)
            {
                string url = Encoding.UTF8.GetString(Convert.FromBase64String(message.MessageText));
                await DownloadFileAsync(url);
                await _queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
            }

            await Task.Delay(1000, stoppingToken); // Throttle the worker to avoid excessive API calls
        }
    }

    private async Task DownloadFileAsync(string url)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var fileName = Path.GetFileName(new Uri(url).AbsolutePath);
                var filePath = Path.Combine("downloaded_files", fileName);

                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                using (var contentStream = await response.Content.ReadAsStreamAsync())
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await contentStream.CopyToAsync(fileStream);
                }

                _logger.LogInformation($"Downloaded file from {url} to {filePath}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error downloading {url}: {ex.Message}");
        }
    }
}
