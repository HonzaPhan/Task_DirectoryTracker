using Task_DirectoryTracker.Abstractions;
using Task_DirectoryTracker.Models;

namespace Task_DirectoryTracker.Services;

public sealed class ScanService(ISnapshotStorage snapshotStorage, IDirectoryScanner scanner, IChangeDetector changeDetector) : IScanService
{
    private readonly ISnapshotStorage _snapshotStorage = snapshotStorage;
    private readonly IDirectoryScanner _scanner = scanner;
    private readonly IChangeDetector _changeDetector = changeDetector;

    public async Task<ScanResult> ScanAsync(string path)
    {
        DirectorySnapshot? previous = await _snapshotStorage.LoadAsync(path);
        DirectorySnapshot current = await _scanner.ScanAsync(path);

        ScanResult result = _changeDetector.DetectChanges(previous, current);

        await _snapshotStorage.SaveAsync(current);

        return result;
    }
}
