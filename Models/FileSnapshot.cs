namespace Task_DirectoryTracker.Models;


/// <summary>
/// FileSnapshot represents a snapshot of a file at a specific point in time.
/// It contains information about the file's path, its hash value (which can be used to detect changes), the version number (which increments with each modification), the last modification time, and the size of the file in bytes.
/// </summary>
public class FileSnapshot
{
    public required string Path { get; init; }
    public required string Hash { get; init; }
    public int Version { get; set; }
    public DateTime LastModification { get; init; }
    public long Size { get; init; }
}
