using System.Diagnostics;

namespace DotNet_CleanArchitecture.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("Handling request: {method} {url}", context.Request.Method, context.Request.Path);

            await _next(context);

            stopwatch.Stop();
            _logger.LogInformation("Finished handling request. Status Code: {statusCode}. Time taken: {elapsed}ms", context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
        }
    }
}
