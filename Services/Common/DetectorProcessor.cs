using Task_DirectoryTracker.Enums;
using Task_DirectoryTracker.Models;

namespace Task_DirectoryTracker.Services.Common;

/// <summary>
/// This static class contains methods for processing the detection of added, modified, and deleted files and directories between two directory snapshots. <br/>
/// It is used by the ChangeDetector service to perform the actual logic of comparing the previous and current snapshots and populating the ScanResult with the detected changes. <br/>
/// It was created to separate the detection logic from the ChangeDetector class and to keep the code organized and maintainable. <br/>
/// </summary>
internal static class DetectorProcessor
{
    /// <summary>
    /// This method detects added and modified files by comparing the current directory snapshot with the previous one.
    /// </summary>
    /// <param name="result"> The ScanResult object that will be populated with the detected added and modified files.</param>
    /// <param name="previousFiles"> A dictionary of the previous files, where the key is the file path and the value is the FileSnapshot object representing the file in the previous snapshot.</param>
    /// <param name="current"> The current DirectorySnapshot object representing the current state of the directory.</param>
    internal static void DetectAddedAndModifiedFiles(ScanResult result, Dictionary<string, FileSnapshot> previousFiles, DirectorySnapshot current)
    {
        foreach (FileSnapshot currentFile in current.Files)
        {
            if (!previousFiles.TryGetValue(currentFile.Path, out FileSnapshot? oldFile))
            {
                HandleAddedFile(result, currentFile);
                continue;
            }

            if (oldFile.Hash != currentFile.Hash)
                HandleModifiedFile(result, currentFile, oldFile);
            else
                currentFile.Version = oldFile.Version;
        }
    }

    internal static void DetectDeletedFiles(ScanResult result, DirectorySnapshot previous, DirectorySnapshot current)
    {
        Dictionary<string, FileSnapshot> currentFiles = current.Files.ToDictionary(x => x.Path);

        foreach (FileSnapshot deletedFile in previous.Files)
        {
            if (!currentFiles.ContainsKey(deletedFile.Path))
                result.DeletedFiles.Add(deletedFile.Path);
        }
    }

    internal static void DetectDeletedDirectories(ScanResult result, DirectorySnapshot previous, DirectorySnapshot current)
    {
        foreach (string deletedDir in previous.Directories)
        {
            if (!current.Directories.Contains(deletedDir))
                result.DeletedDirectories.Add(deletedDir);
        }
    }

    /// <summary>
    /// Private helper method to handle the logic for added files. <br/>
    /// It sets the version of the added file to 1 and adds a new FileChange object to the AddedFiles list in the ScanResult, indicating that the file was added and its version. <br/>
    /// </summary>
    private static void HandleAddedFile(ScanResult result, FileSnapshot currentFile)
    {
        currentFile.Version = 1;
        result.AddedFiles.Add(new FileChange
        {
            Path = currentFile.Path,
            Operation = OperationTypeEnum.Added,
            Version = currentFile.Version
        });
    }

    /// <summary>
    /// Private helper method to handle the logic for modified files. <br/>
    /// It increments the version of the modified file by 1 based on the version from the previous snapshot and adds a new FileChange object to the ModifiedFiles list in the ScanResult, indicating that the file was modified and its new version. <br/>
    /// </summary>
    private static void HandleModifiedFile(ScanResult result, FileSnapshot currentFile, FileSnapshot oldFile)
    {
        currentFile.Version = oldFile.Version + 1;
        result.ModifiedFiles.Add(new FileChange
        {
            Path = currentFile.Path,
            Operation = OperationTypeEnum.Modified,
            Version = currentFile.Version
        });
    }
}
