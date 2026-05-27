namespace Task_DirectoryTracker.Models.Entities;

/// <summary>
/// DirectorySnapshot represents a snapshot of a directory at a specific point in time. 
/// It contains information about the root path, the time it was created, and lists of files and subdirectories contained within that directory.
/// </summary>
public class DirectorySnapshot
{
    public string RootPath { get; set; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public List<FileSnapshot> Files { get; init; } = [];
    public List<string> Directories { get; init; } = [];
}
