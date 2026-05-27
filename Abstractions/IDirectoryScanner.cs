using Task_DirectoryTracker.Models;

namespace Task_DirectoryTracker.Abstractions;

public interface IDirectoryScanner
{
    Task<DirectorySnapshot> ScanAsync(string rootPath);
}