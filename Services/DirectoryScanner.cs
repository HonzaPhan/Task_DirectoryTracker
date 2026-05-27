using Task_DirectoryTracker.Abstractions;
using Task_DirectoryTracker.Models;
using Task_DirectoryTracker.Models.Entities;

namespace Task_DirectoryTracker.Services;

/// <summary>
/// This class is represents a service that scans a directory and creates a snapshot of its structure and files.
/// It uses an IHashService to compute the hash of each file for change detection.
/// </summary>
public sealed class DirectoryScanner(IHashService hashService) : IDirectoryScanner
{
    private readonly IHashService _hashService = hashService;

    /// <summary>
    /// Scans the specified root directory, including all subdirectories, and creates a <see cref="DirectorySnapshot"/> that contains the relative paths, hashes, versions, last modification times, and sizes of all files, as well as the relative paths of all directories.
    /// </summary>
    /// <param name="rootPath"> The path of the root directory to scan. It should be an absolute path.</param>
    /// <returns> A <see cref="DirectorySnapshot"/> containing the structure and file information of the scanned directory.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the specified root path does not exist or contains no directories.</exception>
    public async Task<DirectorySnapshot> ScanAsync(string rootPath)
    {
        List<string> files = [.. Directory.EnumerateFiles(rootPath, "*", SearchOption.AllDirectories)];
        List<string> directories = [.. Directory.EnumerateDirectories(rootPath, "*", SearchOption.AllDirectories)];

        if (directories is { Count: 0 })
            throw new DirectoryNotFoundException("No directories found in the specified root path.");

        DirectorySnapshot snapshot = new()
        {
            RootPath = rootPath,
            CreatedAt = DateTime.UtcNow
        };

        foreach (string file in files)
        {
            FileInfo info = new(file);

            snapshot.Files.Add(new FileSnapshot
            {
                Path = Path.GetRelativePath(rootPath, file),
                Hash = await _hashService.ComputeHashAsync(file),
                Version = 1,
                LastModification = info.LastWriteTimeUtc,
                Size = info.Length
            });
        }

        snapshot.Directories.AddRange(directories.Select(d => Path.GetRelativePath(rootPath, d)));
        return snapshot;
    }
}
