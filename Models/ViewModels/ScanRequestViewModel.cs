using System.ComponentModel.DataAnnotations;

namespace Task_DirectoryTracker.Models.ViewModels;

public class ScanRequestViewModel
{
    [Required(ErrorMessage = "Prosím zadejte cestu k adresáři.")]
    public string Path { get; set; } = string.Empty;
}
