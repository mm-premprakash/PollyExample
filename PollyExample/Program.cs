using Microsoft.OpenApi.Models;
using PollyExample.Policies;

var builder = WebApplication.CreateBuilder(args);

// Register HttpClient with Retry Policy
builder.Services.AddHttpClient("RetryClient")
    .AddPolicyHandler(RetryPolicy.GetRetryPolicy());

// Register HttpClient with Circuit Breaker Policy
builder.Services.AddHttpClient("CircuitBreakerClient")
    .AddPolicyHandler(CircuitBreakerPolicy.GetCircuitBreakerPolicy());

// Register HttpClient with Timeout Policy
builder.Services.AddHttpClient("TimeoutClient")
    .AddPolicyHandler(TimeoutPolicy.GetTimeoutPolicy());

// Register HttpClient with Fallback Policy
builder.Services.AddHttpClient("FallbackClient")
    .AddPolicyHandler(FallbackPolicy.GetFallbackPolicy());


// Add Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Polly API Example", Version = "v1" });
});

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();