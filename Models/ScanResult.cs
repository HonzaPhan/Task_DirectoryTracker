namespace Task_DirectoryTracker.Models;

/// <summary>
/// ScanResult represents the result of scanning a directory for changes.
/// It contains lists of added files, modified files, deleted files, and deleted directories.
/// </summary>
public class ScanResult
{
    public List<FileChange> AddedFiles { get; init; } = [];
    public List<FileChange> ModifiedFiles { get; init; } = [];
    public List<string> DeletedFiles { get; init; } = [];
    public List<string> DeletedDirectories { get; init; } = [];

    public bool HasAddedFiles => AddedFiles is { Count: > 0 };
    public bool HasModifiedFiles => ModifiedFiles is { Count: > 0 };
    public bool HasDeletedFiles => DeletedFiles is { Count: > 0 };
    public bool HasDeletedDirectories => DeletedDirectories is { Count: > 0 };
}
