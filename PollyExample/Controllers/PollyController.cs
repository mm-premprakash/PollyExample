using Microsoft.AspNetCore.Mvc;
using PollyExample.Policies;

namespace PollyExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollyController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PollyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("retry")]
        public async Task<IActionResult> TestRetryPolicy()
        {
            var client = _httpClientFactory.CreateClient("RetryClient");
            var policy = RetryPolicy.GetRetryPolicy();

            var response = await policy.ExecuteAsync(() => client.GetAsync("endpoint")); //setup endpoint

            return Ok($"Response status: {response.StatusCode}");
        }

        [HttpGet("circuitbreaker")]
        public async Task<IActionResult> TestCircuitBreakerPolicy()
        {
            var client = _httpClientFactory.CreateClient("CircuitBreakerClient");
            var policy = CircuitBreakerPolicy.GetCircuitBreakerPolicy();

            var response = await policy.ExecuteAsync(() => client.GetAsync("endpoint")); //setup endpoint

            return Ok($"Response status: {response.StatusCode}");
        }

        [HttpGet("timeout")]
        public async Task<IActionResult> TestTimeoutPolicy()
        {
            var client = _httpClientFactory.CreateClient("TimeoutClient");
            var policy = TimeoutPolicy.GetTimeoutPolicy();

            var response = await policy.ExecuteAsync(() => client.GetAsync("endpoint")); //setup endpoint

            return Ok($"Response status: {response.StatusCode}");
        }

        [HttpGet("fallback")]
        public async Task<IActionResult> TestFallbackPolicy()
        {
            var client = _httpClientFactory.CreateClient("FallbackClient");
            var policy = FallbackPolicy.GetFallbackPolicy();

            var response = await policy.ExecuteAsync(() => client.GetAsync("endpoint")); //setup endpoint

            return Ok($"Response status: {response.StatusCode}");
        }
    }
}