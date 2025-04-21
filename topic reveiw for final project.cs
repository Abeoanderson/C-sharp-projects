//example 1
//async task

using System;
using System.Threading.Tasks;

class Program {
    static async Task Main(string[] args) {
        Console.WriteLine("starting async task...");
        await DoSomethingAsync();

        Console.WriteLine("Async task completed.");
    }
    static async Task DosomethingAsync() {
        await Task.Delay(2000); // wait for 2 seconds

        Console.WriteLine("Task is done");
    }
}



// example 2
// reading from web with async task
using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program {
    static async Task Main(string[] args) {
        Console.WriteLine("starting async web request...");

        string result = await FetchDataFromWebAsync("https://jsonplaceholder.typicode.com/posts");
        Console.WriteLine("Data fetched successfully!!");
        Console.WriteLine($"Result: {result.Substring(0,100)}...");

    }
    static async Task<string> FetchDataFromWebAsync(string url) {
        using(HttpClient client = new HttpClient()) {
            string response = await client.GetStringAsync(url);
            return response;
        }
    }
}

//example 3
//reading from file with async task
using System;
using System.IO;
using System.Threading.Tasks;

class Program {
    static async Task Main(string[] args) {
        Console.WriteLine("Starting async file reading...");
        string filePath = "sample.txt";

        string content = await ReadFileAsync(filePath);

        Console.WriteLine("file read succesffuly");
        Console.WriteLine($"file content: {content.Substring(0,100)}...");
    }
    static async Task<string> ReadFileAsync(string filePath) {
        using (StreamReader reader = new StreamReader(filePath)) {
            return await reader.REadToEndAsync();
        }
    }
}