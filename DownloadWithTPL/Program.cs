using System.Collections.Concurrent;

namespace DownloadWithTPL;

public class DownloadManager
{
    private readonly string _downloadDirectory;
    private readonly int _maxConcurrentDownloads;
    private readonly int _batchSize;

    public DownloadManager(string downloadDirectory, int maxConcurrentDownloads = 10, int batchSize = 1000)
    {
        _downloadDirectory = downloadDirectory;
        _maxConcurrentDownloads = maxConcurrentDownloads;
        _batchSize = batchSize;
    }

    public async Task DownloadFilesAsync(string urlListFilePath)
    {
        var tasks = new List<Task>();
        using (var reader = new StreamReader(urlListFilePath))
        {
            while (!reader.EndOfStream)
            {
                var urlBatch = await ReadUrlBatchAsync(reader, _batchSize);
                if (urlBatch.Any())
                {
                    tasks.Add(ProcessUrlBatchAsync(urlBatch));
                }
            }
        }

        await Task.WhenAll(tasks);
    }

    private async Task<List<string>> ReadUrlBatchAsync(StreamReader reader, int batchSize)
    {
        var urlBatch = new List<string>();
        for (int i = 0; i < batchSize && !reader.EndOfStream; i++)
        {
            var line = await reader.ReadLineAsync() ?? "";
            urlBatch.Add(line);
        }
        return urlBatch;
    }

    private async Task ProcessUrlBatchAsync(List<string> urlBatch)
    {
        var downloadQueue = new BlockingCollection<string>(urlBatch.Count);
        foreach (var url in urlBatch)
        {
            downloadQueue.TryAdd(url);
        }

        for (int i = 0; i < _maxConcurrentDownloads; i++)
        {
            await DownloadFileAsync(downloadQueue);
        }
    }

    private async Task DownloadFileAsync(BlockingCollection<string> downloadQueue)
    {
        using (var httpClient = new HttpClient())
        {
            while (downloadQueue.TryTake(out string? url))
            {
                try
                {
                    var fileName = GetFileNameFromUrl(url);
                    var filePath = Path.Combine(_downloadDirectory, fileName);

                    using (var response = await httpClient.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            await response.Content.CopyToAsync(new FileStream(filePath, FileMode.Create));
                            Console.WriteLine($"Downloaded: {fileName}");
                        }
                        else
                        {
                            Console.WriteLine($"Error downloading {url}: {response.StatusCode}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error downloading {url}: {ex.Message}");
                }
            }
        }
    }

    private static string GetFileNameFromUrl(string url)
    {
        // Implement logic to extract filename from URL (consider potential variations)
        return Path.GetFileName(url);
    }
}