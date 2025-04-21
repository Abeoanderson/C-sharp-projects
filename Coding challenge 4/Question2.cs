using System;
using System.IO;
using System.Threading.Tasks;
public class AsyncIOExample
{
public async Task ReadFileAsync(string filePath)
{
Console.WriteLine("Starting to read file...");
// Use asynchronous file reading to avoid blocking the main thread
using (StreamReader reader = new StreamReader(filePath))
{
string content = await reader.ReadToEndAsync();
Console.WriteLine("File reading completed.");
Console.WriteLine($"File Content: {content.Substring(0, 100)}..."); // Print the first 100 characters of the file
}
}
}
public class Program
{
public static async Task Main(string[] args)
{
// define example of type var and initialize it with new AsyncIOExample();
var example = new AsyncIOExample();
// Use a valid path for an actual file in your environment (You may create this file with some dummy text with

//at least 100 words, for this you can use any content let’s say from web).
string filePath = "somefile.txt";
// Execute the asynchronous file read using await for example.ReadFileAsync(filePath)
await example.ReadFileAsync(filePath);
}
}
