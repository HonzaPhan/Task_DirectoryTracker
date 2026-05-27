using System.ComponentModel.DataAnnotations;

namespace Task_DirectoryTracker.Models;

public class ScanRequestViewModel
{
    [Required(ErrorMessage = "Path is required.")]
    public string Path { get; set; } = string.Empty;
}
