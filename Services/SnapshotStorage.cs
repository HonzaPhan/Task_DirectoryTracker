using System.Text.Json;
using Microsoft.Extensions.Options;
using Task_DirectoryTracker.Abstractions;
using Task_DirectoryTracker.Models.Entities;
using Task_DirectoryTracker.Services.Common;

namespace Task_DirectoryTracker.Services;

/// <summary>
/// This class represents a service that handles the storage of the directory snapshots. <br />
/// It provides methods to save a snapshot to a file and to load a snapshot from a file based on the root path. <br />
/// The snapshots are stored in a specified directory with a specified file extension, as defined in the <see cref="SnapshotSetting"/> that is defined in the appsettings.json file. <br />
/// </summary>
public sealed class SnapshotStorage(IOptions<SnapshotSetting> snapshotSettings) : ISnapshotStorage
{
    private readonly SnapshotSetting _snapshotSettings = snapshotSettings.Value;

    /// Instantiate a JsonSerializerOptions object to configure the JSON serialization and deserialization behavior and to avoid CA1869 warning https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1869
    private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true
    };

    /// <summary>
    /// Saves the given directory snapshot to a file. <br />
    /// </summary>
    /// <param name="snapshot"> The directory snapshot to be saved. </param>
    /// <returns> A task that is going to save the snapshot to a directory with a file extension, as defined in the <see cref="SnapshotSetting"/>. </returns>
    public async Task SaveAsync(DirectorySnapshot snapshot)
    {
        Directory.CreateDirectory(path: _snapshotSettings.Directory);

        string fileName = FileBuilder.BuildFileName(path: snapshot.RootPath, fileExtension: _snapshotSettings.FileExtension);
        string path = Path.Combine(_snapshotSettings.Directory, fileName);
        string json = JsonSerializer.Serialize(snapshot, _jsonOptions);

        await File.WriteAllTextAsync(path, json);
    }

    /// <summary>
    /// Loads a directory snapshot from a file based on the root path. <br />
    /// </summary>
    /// <param name="rootPath"> The root path of the directory snapshot to be loaded. </param>
    /// <returns> A directory snapshot loaded from a file if it exists, otherwise null. </returns>
    public async Task<DirectorySnapshot?> LoadAsync(string rootPath)
    {
        string fileName = FileBuilder.BuildFileName(path: rootPath, fileExtension: _snapshotSettings.FileExtension);
        string path = Path.Combine(_snapshotSettings.Directory, fileName);

        if (!File.Exists(path)) return null;

        string json = await File.ReadAllTextAsync(path);
        DirectorySnapshot? snapshot = JsonSerializer.Deserialize<DirectorySnapshot>(json, _jsonOptions);

        if (snapshot is null)
            return null;

        snapshot.RootPath ??= rootPath;

        return snapshot;
    }
}
