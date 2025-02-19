using System.Text;

namespace DotNet_CleanArchitecture.Middleware
{
    public class LoggingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            // Log request
            await LogRequest(context);

            // Capture response body
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // Call the next middleware in the pipeline
            await _next(context);

            // Log response
            await LogResponse(context, responseBody);

            // Copy the contents of the new memory stream (which contains the response) to the original stream
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            var request = context.Request;

            var requestTime = DateTime.UtcNow;
            var logFilePath = GetLogFilePath(requestTime);

            var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Position = 0;

            var logMessage = new StringBuilder();
            logMessage.AppendLine($"Request Time: {requestTime}");
            logMessage.AppendLine($"Request Method: {request.Method}");
            logMessage.AppendLine($"Request Path: {request.Path}");
            logMessage.AppendLine($"Request Query String: {request.QueryString}");
            logMessage.AppendLine($"Request Body: {requestBody}");
            logMessage.AppendLine();

            WriteLogToFile(logFilePath, logMessage.ToString());
        }

        private static async Task LogResponse(HttpContext context, MemoryStream responseBody)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();
            responseBody.Seek(0, SeekOrigin.Begin);

            var responseTime = DateTime.UtcNow;
            var logFilePath = GetLogFilePath(responseTime);

            var logMessage = new StringBuilder();
            logMessage.AppendLine($"Response Time: {responseTime}");
            logMessage.AppendLine($"Response Status Code: {context.Response.StatusCode}");
            logMessage.AppendLine($"Response Body: {responseBodyText}");
            logMessage.AppendLine();

            WriteLogToFile(logFilePath, logMessage.ToString());
        }

        private static string GetLogFilePath(DateTime dateTime)
        {
            var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs/RequestResponse");
            Directory.CreateDirectory(logDirectory);
            var logFileName = $"log_{dateTime:yyyy-MM-dd}.txt";
            return Path.Combine(logDirectory, logFileName);
        }

        private static void WriteLogToFile(string filePath, string logMessage)
        {
            try
            {
                File.AppendAllText(filePath, logMessage);
            }
            catch (Exception ex)
            {
                // Handle logging exception (e.g., log to console or another logging system)
                Console.WriteLine($"Failed to write log to file: {ex.Message}");
            }
        }
    }
}
