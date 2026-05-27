using Task_DirectoryTracker.Models;

namespace Task_DirectoryTracker.Abstractions;

public interface IScanService
{
    Task<ScanResult> ScanAsync(string path);
}