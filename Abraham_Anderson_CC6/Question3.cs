using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        var numbers = new List<int> { 10000, 15000, 12000, 13000, 16000, 17000, 18000 };

        var cts = new CancellationTokenSource();
        var token = cts.Token;

        Stopwatch stopwatch = Stopwatch.StartNew();

        Task.Run(() =>
        {
            Thread.Sleep(2000);
            Console.WriteLine("User requested cancellation.");
            cts.Cancel();
        });

        try
        {
            Parallel.ForEach(numbers, new ParallelOptions { CancellationToken = token }, number =>
            {
                Console.WriteLine($"Calculating factorial of {number} on thread {Thread.CurrentThread.ManagedThreadId}...");
                var result = CalculateFactorial(number, token);
                Console.WriteLine($"Factorial of {number} computed (length: {result.ToString().Length} digits).");
            });
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Factorial computation was cancelled.");
        }

        stopwatch.Stop();
        Console.WriteLine($"Total elapsed time: {stopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine("Done.");
    }

    static BigInteger CalculateFactorial(int n, CancellationToken token)
    {
        BigInteger result = 1;
        for (int i = 2; i <= n; i++)
        {
            token.ThrowIfCancellationRequested();
            result *= i;
        }
        return result;
    }
}
