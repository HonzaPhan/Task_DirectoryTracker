using Task_DirectoryTracker.Abstractions;
using Task_DirectoryTracker.Enums;
using Task_DirectoryTracker.Models;
using Task_DirectoryTracker.Services.Common;

namespace Task_DirectoryTracker.Services;

/// <summary>
/// This class represents a service that detects changes between previous and current directory snapshots.
/// </summary>
public sealed class ChangeDetector : IChangeDetector
{
    /// <summary>
    /// Detects changes between the previous and current directory snapshots and returns a ScanResult containing the added, modified, and deleted files and directories. <br />
    /// It uses the <see cref="DetectorProcessor"/> to perform the actual detection logic for added, modified, and deleted files and directories.
    /// </summary>
    /// <param name="previous">The previous directory snapshot.</param>
    /// <param name="current">The current directory snapshot.</param>
    /// <returns>A ScanResult containing the detected changes.</returns>
    public ScanResult DetectChanges(DirectorySnapshot? previous, DirectorySnapshot current)
    {
        if (previous is null) return CreateNewScanResult(current);

        ScanResult result = new();
        Dictionary<string, FileSnapshot> previousFiles = previous.Files.ToDictionary(x => x.Path);

        DetectorProcessor.DetectAddedAndModifiedFiles(result, previousFiles, current);
        DetectorProcessor.DetectDeletedFiles(result, previous, current);
        DetectorProcessor.DetectDeletedDirectories(result, previous, current);

        return result;
    }

    /// <summary>
    /// Private method that creates a new ScanResult for the case when there is no previous snapshot, meaning all files in the current snapshot are considered as added.
    /// </summary>
    /// <param name="current"> The current directory snapshot.</param>
    /// <returns> A ScanResult with all files in the current snapshot marked as added.</returns>
    private static ScanResult CreateNewScanResult(DirectorySnapshot current)
    {
        ScanResult result = new();

        foreach (FileSnapshot file in current.Files)
        {
            result.AddedFiles.Add(new FileChange
            {
                Path = file.Path,
                Operation = OperationTypeEnum.Added,
                Version = file.Version
            });
        }

        return result;
    }
}
