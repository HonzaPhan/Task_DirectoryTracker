using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Task_DirectoryTracker.Middlewares;

internal sealed record ErrorResponse(string Error, int StatusCode, string TraceId);

internal static class ExceptionMiddlewareExtension
{
    public static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        Type type = ex.GetType();

        if (Map.TryGetValue(type, out (int StatusCode, string? DefaultMessage) mapping))
        {
            await WriteErrorResponse(context, mapping.StatusCode, mapping.DefaultMessage ?? ex.Message);
            return;
        }

        await WriteErrorResponse(context, StatusCodes.Status500InternalServerError, "Unexpected error.");
    }

    public static async Task WriteErrorResponse(HttpContext context, int statusCode, string message)
    {
        ErrorResponse errorResponse = new(Error: message, StatusCode: statusCode, TraceId: context.TraceIdentifier);
        await context.Response.WriteAsJsonAsync(errorResponse);
    }

    private static readonly Dictionary<Type, (int StatusCode, string? DefaultMessage)> Map = new()
    {
        { typeof(FileNotFoundException), (StatusCodes.Status404NotFound, null) },
        { typeof(DirectoryNotFoundException), (StatusCodes.Status404NotFound, null) },
        { typeof(JsonException), (StatusCodes.Status400BadRequest, "Invalid JSON.") },
        { typeof(ValidationException), (StatusCodes.Status400BadRequest, "Validation failed.") },
        { typeof(InvalidOperationException), (StatusCodes.Status400BadRequest, "Invalid operation.") },
    };
}
