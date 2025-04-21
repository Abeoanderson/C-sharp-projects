using System;
using System.Threading.Tasks;

public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class AsyncExample
{
    public async Task DownloadDataAsync()
    {
        Console.WriteLine("Starting data download..");
        await Task.Delay(2000);
        Console.WriteLine("Data download completed.");
    }

    public async Task<User> GetUserDetailsAsync(int userId)
    {
        Console.WriteLine($"Fetching details for user {userId}...");
        await Task.Delay(1000);
        return new User { Name = "John Doe", Age = 30 };
    }

    public async void ButtonClickHandlerAsync(object sender, EventArgs e)
    {
        Console.WriteLine("Button clicked, starting async operation...");
        await Task.Delay(1000);
        Console.WriteLine("Button click operation completed.");
    }
}

public class Program
{

    public static async Task Main(string[] args)
    {

        var example = new AsyncExample();

        await example.DownloadDataAsync();

        User user = await example.GetUserDetailsAsync(1);
        Console.WriteLine($"User Details: Name = {user.Name}, Age = {user.Age}");

        example.ButtonClickHandlerAsync(null, EventArgs.Empty);

        await Task.Delay(2000);
    }
}