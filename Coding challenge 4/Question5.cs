using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;

public class AsyncFileDownloader
{
    private static readonly HttpClient client = new HttpClient();

    // Asynchronous method to download a large file
    public async Task DownloadFileAsync(string url, string destinationPath)
    {
        HttpResponseMessage response = null;
        FileStream fileStream = null;
        Stream stream = null;
        try
        {
            Console.WriteLine("Starting file download...");

            // Send the GET request asynchronously
            //define var 
            response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            
            // Ensure a successful response
            response.EnsureSuccessStatusCode();

            // Open a stream to write the downloaded file to disk
            fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None);

            // Open a stream to read the downloaded content
            //define stream and initialize it with await for response.Content.ReadAsStreamAsync();
            stream = await response.Content.ReadAsStreamAsync();
            
            // Read and write the file in chunks to avoid high memory usage
            // await for stream.CopyToAsync(fileStream);
            await stream.CopyToAsync(fileStream);

            Console.WriteLine("File downloaded successfully!");
        }
        catch (HttpRequestException e)
        {
            // Handle HTTP request errors
            Console.WriteLine($"Error during HTTP request: {e.Message}");
        }
        catch (IOException e)
        {
            // Handle file I/O errors (e.g., permission issues, disk full)
            Console.WriteLine($"Error writing to file: {e.Message}");
        }
        catch (Exception e)
        {
            // Handle any other unexpected errors
            Console.WriteLine($"Unexpected error: {e.Message}");
        }
        finally
        {
            // Explicitly dispose of resources
            if (stream != null)
                stream.Dispose();
            if (fileStream != null)
                fileStream.Dispose();
            if (response != null)
                response.Dispose();
        }
    }

    public static async Task Main(string[] args)
    {
        // define downloader of type var and initialize it with new AsyncFileDownloader();
        var downloader = new AsyncFileDownloader();
        
        string fileUrl = "http://ipv4.download.thinkbroadband.com/1GB.zip";
        string destinationPath = "1GB.zip"; // Path to save the file locally

        // Start the asynchronous download
        await downloader.DownloadFileAsync(fileUrl, destinationPath);
    }
}