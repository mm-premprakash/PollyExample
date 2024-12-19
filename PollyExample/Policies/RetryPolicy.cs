using Polly;

namespace PollyExample.Policies
{
    public static class RetryPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                         .RetryAsync(3, (exception, retryCount) =>
                         {
                             Console.WriteLine($"Retrying due to failure. Retry Count: {retryCount}");
                         });
        }
    }
}
