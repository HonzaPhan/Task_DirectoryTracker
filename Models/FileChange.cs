using Task_DirectoryTracker.Enums;

namespace Task_DirectoryTracker.Models;

/// <summary>
/// FileChange represents a change that has occurred to a file in the directory.
/// It contains the path of the file that changed, the type of the operation that occurred and the version of the file after the change.
/// The version property changes with each modification to the file by incrementing the version number, allowing us to track the history of changes to the file over time.
/// </summary>
public class FileChange
{
    public required string Path { get; init; }
    public required OperationTypeEnum Operation { get; init; }
    public int Version { get; init; }
}
