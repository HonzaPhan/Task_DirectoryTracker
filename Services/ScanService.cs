using Task_DirectoryTracker.Abstractions;
using Task_DirectoryTracker.Models.Entities;

namespace Task_DirectoryTracker.Services;

/// <summary>
/// This class represents a service that orchestrates the scanning of a directory, the detection of changes, and the storage of the snapshots.
/// </summary>
public sealed class ScanService(ISnapshotStorage snapshotStorage, IDirectoryScanner scanner, IChangeDetector changeDetector) : IScanService
{
    private readonly ISnapshotStorage _snapshotStorage = snapshotStorage;
    private readonly IDirectoryScanner _scanner = scanner;
    private readonly IChangeDetector _changeDetector = changeDetector;

    /// <summary>
    /// Scans the specified directory, detects changes compared to the previous snapshot, and returns the result. It also saves the new snapshot for future comparisons.
    /// </summary>
    /// <param name="path"> The path of the directory to scan. </param>
    /// <returns> A <see cref="ScanResult"/> object containing the detected changes. </returns>
    public async Task<ScanResult> ScanAsync(string path)
    {
        DirectorySnapshot? previous = await _snapshotStorage.LoadAsync(path);
        DirectorySnapshot current = await _scanner.ScanAsync(path);

        ScanResult result = _changeDetector.DetectChanges(previous, current);

        await _snapshotStorage.SaveAsync(current);

        return result;
    }
}
