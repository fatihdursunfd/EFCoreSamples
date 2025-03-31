using Benchmark;
using Benchmark.Benchmarks;
using BenchmarkDotNet.Running;

internal class Program
{
    private static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<LocationServiceBenchmark>();
    }
}