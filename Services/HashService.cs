using System.Security.Cryptography;
using Task_DirectoryTracker.Abstractions;

namespace Task_DirectoryTracker.Services;

/// <summary>
/// This class represents a service that computes the hash of a file using the SHA256 algorithm. <br />
/// It implements the IHashService interface, which defines a method for computing the hash of a file given its path. <br />
/// The computed hash is returned as a hexadecimal string.
/// </summary>
public sealed class HashService : IHashService
{
    /// <summary>
    /// Computes the hash of a file using the SHA256 algorithm.
    /// </summary>
    /// <param name="filePath">The path of the file for which to compute the hash.</param>
    /// <returns>The hexadecimal string representing the computed hash.</returns>
    public async Task<string> ComputeHashAsync(string filePath)
    {
        await using FileStream stream = File.OpenRead(filePath);
        using SHA256 sha = SHA256.Create();
        byte[] hash = await sha.ComputeHashAsync(stream);
        return Convert.ToHexString(hash);
    }
}