namespace DotNet_CleanArchitecture.Middleware
{
    public class RoutingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;

            if (path == "/api/home")
            {
                await context.Response.WriteAsync("Welcome to the Home Page");
                return;
            }

            await _next(context);
        }
    }
}
