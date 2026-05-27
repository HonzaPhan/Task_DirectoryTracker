using Task_DirectoryTracker.Models.Entities;

namespace Task_DirectoryTracker.Abstractions;

public interface IScanService
{
    Task<ScanResult> ScanAsync(string path);
}