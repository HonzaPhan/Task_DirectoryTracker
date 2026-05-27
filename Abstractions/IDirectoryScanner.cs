using Task_DirectoryTracker.Models.Entities;

namespace Task_DirectoryTracker.Abstractions;

public interface IDirectoryScanner
{
    Task<DirectorySnapshot> ScanAsync(string rootPath);
}