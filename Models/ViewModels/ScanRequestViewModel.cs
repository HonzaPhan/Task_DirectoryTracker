using System.ComponentModel.DataAnnotations;

namespace Task_DirectoryTracker.Models.ViewModels;

public class ScanRequestViewModel
{
    [Required(ErrorMessage = "Path is required.")]
    public string Path { get; set; } = string.Empty;
}
