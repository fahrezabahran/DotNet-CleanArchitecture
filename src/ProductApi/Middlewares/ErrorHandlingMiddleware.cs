namespace DotNet_CleanArchitecture.Middlewares
{
    public class ErrorHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Tangani kesalahan dan log
                var errorLogFilePath = Path.Combine("Logs", "Error", $"{DateTime.Now:ddMMyyyy_HHmmss}.txt");
                var errorLogEntry = $"{DateTime.Now:HH:mm:ss} - Error: {ex.Message}\n{ex.StackTrace}\n";
                await File.AppendAllTextAsync(errorLogFilePath, errorLogEntry);

                context.Response.StatusCode = 500; // Internal Server Error
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        }
    }
}
