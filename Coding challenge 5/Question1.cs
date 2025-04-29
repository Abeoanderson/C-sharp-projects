using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    // Simulates a long-running task that checks for cancellation
    static async Task RunWorkAsync(CancellationToken token)
    {
        Console.WriteLine("Work started...\n");
        for (int i = 1; i <= 10; i++)
        {
            token.ThrowIfCancellationRequested(); // Check if cancellation was requested
            Console.WriteLine($"Step {i}/10 in progress...");
            await Task.Delay(1000, token); // Simulate async work
        }
        Console.WriteLine("\nWork completed successfully.");
    }

    static async Task Main(string[] args)
    {
        using CancellationTokenSource cts = new CancellationTokenSource();
        Console.WriteLine("Press ENTER at any time to cancel the operation.\n");

        // Start the long-running task with the token:
        Task workTask = RunWorkAsync(cts.Token);

        // Wait for either the Enter key or the work to complete:
        Task cancelTask = Task.Run(() => Console.ReadLine());

        Task finishedTask = await Task.WhenAny(workTask, cancelTask);

        // If Enter was pressed first, cancel the operation
        if (finishedTask == cancelTask)
        {
            cts.Cancel(); // call Cancel() method from cts
            Console.WriteLine("\nCancellation requested by user.");
        }

        // Await the work task to observe any exceptions
        try
        {
            await workTask;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\nOperation was cancelled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nUnexpected error: {ex.Message}");
        }

        Console.WriteLine("\nProgram has ended. Press any key to exit.");
        Console.ReadKey();
    }
}