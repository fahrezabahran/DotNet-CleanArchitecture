namespace DotNet_CleanArchitecture.Middleware
{
    public class LoggingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.UtcNow;

            // Log sebelum memproses permintaan
            Console.WriteLine($"Request started at {startTime}");

            await _next(context);

            var endTime = DateTime.UtcNow;
            var duration = endTime - startTime;

            // Log setelah memproses permintaan
            Console.WriteLine($"Request ended at {endTime}. Duration: {duration.TotalMilliseconds} ms");
        }
    }
}
