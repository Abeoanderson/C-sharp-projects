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
Console.WriteLine("Starting file download...");
// Send the GET request asynchronously
using (HttpResponseMessage response = await client.GetAsync(url,
HttpCompletionOption.ResponseHeadersRead))
{
response.EnsureSuccessStatusCode(); // Ensure a successful response
// Open a stream to write the downloaded file
using (var fileStream = new FileStream(destinationPath, FileMode.Create,
FileAccess.Write, FileShare.None))
{
// Open a stream to read the downloaded content
using (var stream = await response.Content.ReadAsStreamAsync())
{
// Read and write the file in chunks to avoid high memory usage
// await for stream.CopyToAsync(fileStream);
await stream.CopyToAsync(fileStream);
Console.WriteLine("File downloaded successfully!");
}
}
}
}
public static async Task Main(string[] args)
{
// define downloader of type var and initialize it with new AsyncFileDownloader();
var downloader = new AsyncFileDownloader();
//define fileUrl of type string and initialize it with
string fileUrl = "http://ipv4.download.thinkbroadband.com/1GB.zip"
// URL of the file to download
string destinationPath = "1GB.zip"; // Path to save the file locally
// Start the asynchronous download
await downloader.DownloadFileAsync(fileUrl, destinationPath);
}
}


// The main differences I see are error handling, and Explicit Resource Disposal. For example 
// in question 4 we use using so that we dont have to instantiate or take care of our stream
// also question 4 doesnt have as good of error handling. In contrast, question 5, we handle 
// opening our resources and instatiatng them, and getting rid of them, for example in the finally block 