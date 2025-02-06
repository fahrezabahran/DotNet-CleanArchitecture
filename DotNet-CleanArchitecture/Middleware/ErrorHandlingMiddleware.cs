namespace DotNet_CleanArchitecture.Middleware
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
                context.Response.StatusCode = 500; // Internal Server Error
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        }
    }
}
