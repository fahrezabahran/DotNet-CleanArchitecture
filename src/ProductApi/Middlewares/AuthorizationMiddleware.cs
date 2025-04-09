namespace DotNet_CleanArchitecture.Middlewares
{
    public class AuthorizationMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.User.IsInRole("Admin"))
            {
                context.Response.StatusCode = 403; // Forbidden
                await context.Response.WriteAsync("Forbidden");
                return;
            }

            await _next(context);
        }
    }
}
