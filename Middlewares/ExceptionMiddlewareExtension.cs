using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Task_DirectoryTracker.Middlewares;

internal sealed record ErrorResponse(string Error, int StatusCode, string TraceId);

/// <summary>
/// ExceptionMiddlewareExtension is a static class that provides methods for handling exceptions in the middleware pipeline of an ASP.NET Core application.
/// It contains a method HandleExceptionAsync that takes an HttpContext and an Exception as parameters and writes an appropriate error response based on the type of the exception. 
/// </summary>
internal static class ExceptionMiddlewareExtension
{
    /// <summary>
    /// Handles exceptions by checking the type of the exception against a predefined mapping of exception types to HTTP status codes and default messages.
    /// </summary>
    /// <param name="context"> The HttpContext of the current request, which is used to write the error response.</param>
    /// <param name="ex"> The exception that was thrown during the processing of the request. The method checks the type of this exception against a predefined mapping to determine the appropriate HTTP status code and error message to return in the response.</param>
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

    /// <summary>
    /// Map is a static readonly dictionary that maps specific exception types to corresponding HTTP status codes and default error messages.
    /// </summary>
    private static readonly Dictionary<Type, (int StatusCode, string? DefaultMessage)> Map = new()
    {
        { typeof(FileNotFoundException), (StatusCodes.Status404NotFound, null) },
        { typeof(DirectoryNotFoundException), (StatusCodes.Status404NotFound, null) },
        { typeof(JsonException), (StatusCodes.Status400BadRequest, "Invalid JSON.") },
        { typeof(ValidationException), (StatusCodes.Status400BadRequest, "Validation failed.") },
        { typeof(InvalidOperationException), (StatusCodes.Status400BadRequest, "Invalid operation.") },
    };
}
