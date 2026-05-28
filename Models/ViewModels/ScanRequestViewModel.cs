using System.ComponentModel.DataAnnotations;
using Task_DirectoryTracker.Validation;

namespace Task_DirectoryTracker.Models.ViewModels;

public class ScanRequestViewModel
{
    [Required(ErrorMessage = "Prosím zadejte cestu k adresáři.")]
    [MaxLength(260, ErrorMessage = "Cesta nesmí být delší než 260 znaků.")]
    [SafeLocalPathAttribute]
    public string Path { get; set; } = string.Empty;
}
