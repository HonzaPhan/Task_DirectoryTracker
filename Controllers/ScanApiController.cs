using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Task_DirectoryTracker.Abstractions;
using Task_DirectoryTracker.Models.Entities;
using Task_DirectoryTracker.Models.ViewModels;

namespace Task_DirectoryTracker.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/scan")]
public sealed class ScanApiController(IScanService scanService) : ControllerBase
{
    private readonly IScanService _scanService = scanService;

    [HttpPost]
    public async Task<IActionResult> Scan([FromBody] ScanRequestViewModel request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Result<ScanResult> result = await _scanService.ScanAsync(request.Path);

        return result.Match<IActionResult>(
            onSuccess: Ok,
            onFailure: error => BadRequest(new { field = error.Field, error = error.Message })
        );
    }
}
