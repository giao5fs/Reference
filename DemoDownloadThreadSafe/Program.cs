using Newtonsoft.Json;

namespace DemoDownloadThreadSafe;

public class Program
{
    public static async Task Main(string[] args)
    {
        string urlFilePath = "urls.txt"; // Path to your file containing URLs
        string checkpointFilePath = "checkpoint.json"; // Path to the checkpoint file
        int batchSize = 1000; // Number of URLs to process in each batch
        int maxConcurrency = 100; // Maximum number of concurrent downloads

        var downloader = new Program();
        await downloader.DownloadFilesWithCheckpointingAsync(urlFilePath, checkpointFilePath, batchSize, maxConcurrency);

        Console.WriteLine("All files downloaded.");
    }

    public IEnumerable<string> StreamUrlsFromFile(string filePath)
    {
        using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        using (var streamReader = new StreamReader(fileStream))
        {
            string line = streamReader.ReadLine() ?? "";

            while (line != null)
            {
                yield return line;
            }
        }
    }

    public async Task DownloadFilesWithCheckpointingAsync(string urlFilePath, string checkpointFilePath, int batchSize, int maxConcurrency)
    {
        var checkpoint = Checkpoint.Load(checkpointFilePath) ?? new Checkpoint();
        var urls = StreamUrlsFromFile(urlFilePath).Skip((int)checkpoint.LastProcessedLine);

        using (var semaphore = new SemaphoreSlim(maxConcurrency))
        {
            var batch = new List<string>(batchSize);
            long lineNumber = checkpoint.LastProcessedLine;

            foreach (var url in urls)
            {
                if (checkpoint.ProcessedUrls.Contains(url))
                {
                    lineNumber++;
                    continue;
                }

                batch.Add(url);
                if (batch.Count >= batchSize)
                {
                    await ProcessBatch(batch, semaphore, checkpoint, lineNumber, checkpointFilePath);
                    batch.Clear();
                }

                lineNumber++;
            }

            // Process remaining URLs in the batch
            if (batch.Count > 0)
            {
                await ProcessBatch(batch, semaphore, checkpoint, lineNumber, checkpointFilePath);
            }
        }
    }

    private async Task ProcessBatch(List<string> batch, SemaphoreSlim semaphore, Checkpoint checkpoint, long lineNumber, string checkpointFilePath)
    {
        var tasks = batch.Select(url => DownloadFileAsync(url, semaphore));
        await Task.WhenAll(tasks);

        foreach (var url in batch)
        {
            checkpoint.ProcessedUrls.Add(url);
        }

        checkpoint.LastProcessedLine = lineNumber;
        checkpoint.Save(checkpointFilePath);
    }

    private async Task DownloadFileAsync(string url, SemaphoreSlim semaphore)
    {
        await semaphore.WaitAsync();
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
            }
        }
        catch (Exception ex)
        {
            // Handle exception (e.g., log it)
            Console.WriteLine($"Error downloading {url}: {ex.Message}");
        }
        finally
        {
            semaphore.Release();
        }
    }
}


public class Checkpoint
{
    public HashSet<string> ProcessedUrls { get; set; } = new HashSet<string>();
    public long LastProcessedLine { get; set; } = 0;

    public void Save(string filePath)
    {
        var json = JsonConvert.SerializeObject(this);
        File.WriteAllText(filePath, json);
    }

    public static Checkpoint? Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new Checkpoint();
        }

        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<Checkpoint>(json);
    }
}