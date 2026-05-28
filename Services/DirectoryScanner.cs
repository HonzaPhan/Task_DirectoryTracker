using Task_DirectoryTracker.Abstractions;
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
    /// <exception cref="DirectoryNotFoundException">Thrown if <paramref name="rootPath"/> does not exist.</exception>
    public async Task<DirectorySnapshot> ScanAsync(string rootPath)
    {
        if (!Directory.Exists(rootPath))
            throw new DirectoryNotFoundException($"Root path '{rootPath}' does not exist.");

        IEnumerable<string> files = [.. Directory.EnumerateFiles(rootPath, "*", SearchOption.AllDirectories)];
        IEnumerable<string> directories = [.. Directory.EnumerateDirectories(rootPath, "*", SearchOption.AllDirectories)];

        DirectorySnapshot snapshot = new()
        {
            RootPath = rootPath,
            CreatedAt = DateTime.UtcNow
        };

        FileSnapshot[] snapshots = await Task.WhenAll(files.Select(async file =>
        {
            FileInfo info = new(file);
            return new FileSnapshot
            {
                Path = Path.GetRelativePath(rootPath, file),
                Hash = await _hashService.ComputeHashAsync(file),
                Version = 1,
                LastModification = info.LastWriteTimeUtc,
                Size = info.Length
            };
        }));

        snapshot.Files.AddRange(snapshots);

        snapshot.Directories.AddRange(directories.Select(d => Path.GetRelativePath(rootPath, d)));
        return snapshot;
    }
}
