using Polly.Timeout;
using Polly;

namespace PollyExample.Policies
{
    public static class TimeoutPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy()
        {
            //return Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(5), TimeoutStrategy.Pessimistic,
            //    onTimeoutAsync: (context, timespan, task) =>
            //    {
            //        Console.WriteLine($"Request timed out after {timespan.TotalSeconds} seconds.");
            //        return Task.CompletedTask;
            //    });
            return Policy.TimeoutAsync<HttpResponseMessage>(
            TimeSpan.FromSeconds(5), // Example timeout duration
            TimeoutStrategy.Pessimistic,
            onTimeoutAsync: async (context, timespan, task) =>
            {
                Console.WriteLine($"Request timed out after {timespan.TotalSeconds} seconds.");
            });
        }
    }
}
