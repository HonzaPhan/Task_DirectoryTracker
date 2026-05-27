namespace Task_DirectoryTracker.Models;

/// <summary>
/// Defines the settings for snapshot storage, including the file extension for snapshot files and the directory where snapshots are stored. <br />
/// The SnapshotSetting is defined in the appsettings.json file.
/// </summary>
public class SnapshotSetting
{
    public string FileExtension { get; init; } = string.Empty;
    public string Directory { get; init; } = string.Empty;
}
