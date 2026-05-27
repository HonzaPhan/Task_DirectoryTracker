namespace Task_DirectoryTracker.Models.ViewModels;

public sealed class StringListCardViewModel
{
    public required string Title { get; init; }
    public required string Emoji { get; init; }
    public required string BorderClass { get; init; }
    public required string HeaderClass { get; init; }
    public required string EmptyMessage { get; init; }
    public IReadOnlyList<string> Items { get; init; } = [];
    public bool HasItems => Items is { Count: > 0 };
}