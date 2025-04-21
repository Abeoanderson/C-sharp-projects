using System;
using System.Net.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class AsyncFileDownloader
{
    private static readonly HttpClient client = new HttpClient();

    // Asynchronous method to download a large file with cancellation support
    public async Task DownloadFileAsync(string url, string destinationPath, CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting file download...");

        // Add headers to avoid 403 Forbidden errors
        if (!client.DefaultRequestHeaders.Contains("User-Agent"))
        {
            client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 " +
                "(KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
        }

        // Send the GET request asynchronously with cancellation support
        using (HttpResponseMessage response = await client.GetAsync(
            url, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
        {
            response.EnsureSuccessStatusCode(); // Ensure a successful response

            // Open a stream to write the downloaded file
            using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Open a stream to read the downloaded content
                using (var stream = await response.Content.ReadAsStreamAsync(cancellationToken))
                {
                    // Read and write the file in chunks with cancellation support
                    await stream.CopyToAsync(fileStream, 81920, cancellationToken);
                    Console.WriteLine("File downloaded successfully!");
                }
            }
        }
    }

    public static async Task Main(string[] args)
    {
        var downloader = new AsyncFileDownloader();
        string fileUrl = "http://ipv4.download.thinkbroadband.com/1GB.zip"; // URL of the file to download
        string destinationPath = "1GB.zip"; // Path to save the file locally

        // with the using keyword define cts of type CancellationTokenSource
        // initialize it using new keyword and calling the constructor method
        using CancellationTokenSource cts = new CancellationTokenSource();

        // Optional: Cancel after a specific time
        cts.CancelAfter(TimeSpan.FromSeconds(10)); // Auto-cancel after 10 seconds

        Console.WriteLine("Press any key to cancel the download...");

        // define keyPressTask of type var and initialize it with Task.Run
        // and send ()=>Console.ReadKey(true) to it
        var keyPressTask = Task.Run(() => Console.ReadKey(true));

        var downloadTask = downloader.DownloadFileAsync(fileUrl, destinationPath, cts.Token);
        var completedTask = await Task.WhenAny(downloadTask, keyPressTask);

        if (completedTask == keyPressTask)
        {
            cts.Cancel(); // call Cancel() method using cts object
            Console.WriteLine("Cancellation requested.");
        }

        try
        {
            await downloadTask;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Download canceled by user.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Download failed: {ex.Message}");
        }
    }
}