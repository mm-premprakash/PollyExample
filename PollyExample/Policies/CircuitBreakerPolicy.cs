using Polly;

namespace PollyExample.Policies
{
    public static class CircuitBreakerPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                         .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10),
                             onBreak: (result, timespan) =>
                             {
                                 Console.WriteLine($"Circuit Breaker triggered. Break for {timespan.TotalSeconds} seconds.");
                             },
                             onReset: () =>
                             {
                                 Console.WriteLine("Circuit Breaker reset.");
                             });
        }
    }
}
