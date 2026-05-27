namespace Task_DirectoryTracker.Models;

public class ScanPageViewModel
{
    public ScanRequestViewModel Request { get; init; } = new();
    public ScanResult? Result { get; init; }
    public bool HasResult => Result is not null;
}
