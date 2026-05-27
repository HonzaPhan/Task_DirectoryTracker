using Task_DirectoryTracker.Models;

namespace Task_DirectoryTracker.Abstractions;

public interface IChangeDetector
{
    ScanResult DetectChanges(DirectorySnapshot? previous, DirectorySnapshot current);
}