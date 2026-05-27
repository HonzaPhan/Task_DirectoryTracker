using Task_DirectoryTracker.Models.Entities;

namespace Task_DirectoryTracker.Abstractions;

public interface ISnapshotStorage
{
    Task<DirectorySnapshot?> LoadAsync(string rootPath);
    Task SaveAsync(DirectorySnapshot snapshot);
}