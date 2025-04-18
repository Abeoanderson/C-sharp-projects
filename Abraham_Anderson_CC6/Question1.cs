using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        var items = new List<int>();
        for (int i = 0; i < 100; i++) items.Add(i);

        var cts = new CancellationTokenSource();

        Task.Run(() =>
        {
            Thread.Sleep(1000);
            Console.WriteLine("Cancelling...");
            cts.Cancel();
        });

        try
        {
 
            Parallel.ForEach(items, new ParallelOptions { CancellationToken = cts.Token }, item =>
            {

                Console.WriteLine($"Processing item {item}");
                Thread.Sleep(200); 

       
                cts.Token.ThrowIfCancellationRequested();
            });
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation was cancelled.");
        }

        Console.WriteLine("Done.");
    }
}
