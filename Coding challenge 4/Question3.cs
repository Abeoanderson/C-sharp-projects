using System;
using System.Threading.Tasks;
public class AsyncCPUExample
{
// A CPU-bound task: Finding prime numbers up to a specified limit
public async Task FindPrimesAsync(int max)
{
Console.WriteLine("Starting prime number calculation...");
// Use Task.Run to offload CPU-bound work to a separate thread to avoid blocking the
// main thread: define 
var primes =  await Task.Run(() => FindPrimes(max));
Console.WriteLine($"Found {primes.Count} primes up to {max}:");
foreach (var prime in primes)
{
Console.WriteLine(prime);
}
}
// A simple method to find prime numbers
private List<int> FindPrimes(int max)
{
List<int> primes = new List<int>();
for (int i = 2; i <= max; i++)
{
if (IsPrime(i))
{
primes.Add(i);
}
}
return primes;
}
// A method to check if a number is prime
private bool IsPrime(int number)
{
if (number <= 1) return false;
for (int i = 2; i <= Math.Sqrt(number); i++)
{
if (number % i == 0) return false;
}
return true;
}
}
public class Program
{
public static async Task Main(string[] args)
{
// define example of type var and initialize it with new AsyncCPUExample()
var example = new AsyncCPUExample();
// Execute the asynchronous CPU-bound task to find primes up to 1000
// await for example.FindPrimesAsync(1000);
await example.FindPrimesAsync(1000);
}
}
