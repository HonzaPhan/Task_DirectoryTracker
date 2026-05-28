namespace Task_DirectoryTracker.Models.Entities;

public sealed record Error(string Message, string? Field = null)
{
    public static readonly Error None = new(string.Empty);
    public static Error Validation(string message, string field) => new(message, field);
    public static Error NotFound(string message) => new(message);
}
