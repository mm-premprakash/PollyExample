using Polly;

namespace PollyExample.Policies
{
    public static class FallbackPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
        {
            return Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                         .FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK),
                             onFallbackAsync: (outcome, context) =>
                             {
                                 Console.WriteLine("Fallback policy executed.");
                                 return Task.CompletedTask;
                             });
        }
    }
}
