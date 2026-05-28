using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Task_DirectoryTracker.Validation;

public sealed partial class SafeLocalPathAttribute : ValidationAttribute
{
    private static readonly Regex InvalidPathCharsRegex = SafeLocalPathRegex();

    [GeneratedRegex(@"[\x00-\x1F]")]
    private static partial Regex SafeLocalPathRegex();

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string path || string.IsNullOrWhiteSpace(path))
            return new ValidationResult("Neplatná cesta. Cesta nesmí být prázdná nebo obsahovat pouze mezery.");

        if (InvalidPathCharsRegex.IsMatch(path))
            return new ValidationResult("Cesta obsahuje neplatné znaky.");

        return ValidationResult.Success;
    }
}
