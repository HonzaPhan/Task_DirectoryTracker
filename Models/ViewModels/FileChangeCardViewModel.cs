using Task_DirectoryTracker.Models.Entities;

namespace Task_DirectoryTracker.Models.ViewModels;

public sealed class FileChangeCardViewModel
{
    public required string Title { get; init; }
    public required string Emoji { get; init; }
    public required string BorderClass { get; init; }
    public required string HeaderClass { get; init; }
    public required string EmptyMessage { get; init; }
    public IReadOnlyList<FileChange> Files { get; init; } = [];
    public bool HasItems => Files.Count > 0;
}
