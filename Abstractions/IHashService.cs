namespace Task_DirectoryTracker.Abstractions;

public interface IHashService
{
    Task<string> ComputeHashAsync(string filePath);
}