namespace Task_DirectoryTracker.Middlewares;

/// <summary>
/// Global exception handling middleware that catches unhandled exceptions during the request processing pipeline and returns a standardized error response to the client.
/// </summary>
public sealed class ExceptionMiddleware(RequestDelegate next)
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
            await ExceptionMiddlewareExtension.HandleExceptionAsync(context, ex);
        }
    }
}
