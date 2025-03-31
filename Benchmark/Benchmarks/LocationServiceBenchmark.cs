using BenchmarkDotNet.Attributes;
using Domain.Enums;

namespace Benchmark.Benchmarks
{
    public class LocationServiceBenchmark
    {
        private static readonly HttpClient _httpClient = new();
        private string _url = "https://localhost:7207/api/Location/Country/";

        [Benchmark]
        public async Task GetCountryByIdBenchmark()
        {
            var response = await _httpClient.GetAsync($"{_url}{229}/{Language.tr}");
            response.EnsureSuccessStatusCode();
        }

        [Benchmark]
        public async Task GetCountryByIdWithCompiledQueryBenchmark()
        {
            var response = await _httpClient.GetAsync($"{_url}CompiledQuery/{229}/{Language.tr}");
            response.EnsureSuccessStatusCode();
        }

        [Benchmark]
        public async Task GetAllCountryiesBenchmark()
        {
            var response = await _httpClient.GetAsync($"{_url}All/{Language.tr}");
            response.EnsureSuccessStatusCode();
        }

        [Benchmark]
        public async Task GetAllCountriesWithCompiledQueryBenchmark()
        {
            var response = await _httpClient.GetAsync($"{_url}CompiledQuery/All/{Language.tr}");
            response.EnsureSuccessStatusCode();
        }
    }
}