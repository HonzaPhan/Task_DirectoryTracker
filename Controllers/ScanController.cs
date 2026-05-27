using Microsoft.AspNetCore.Mvc;
using Task_DirectoryTracker.Abstractions;
using Task_DirectoryTracker.Models.Entities;
using Task_DirectoryTracker.Models.ViewModels;

namespace Task_DirectoryTracker.Controllers;

public sealed class ScanController(IScanService scanService) : Controller
{
    private readonly IScanService _scanService = scanService;

    [HttpGet]
    public IActionResult Index() => View(new ScanPageViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ScanRequestViewModel request)
    {
        if (!ModelState.IsValid)
            return View(new ScanPageViewModel { Request = request });

        string reqPath = request.Path;

        if (!Directory.Exists(reqPath))
        {
            ModelState.AddModelError(nameof(reqPath), "Directory does not exist.");
            return View(new ScanPageViewModel { Request = request });
        }

        ScanResult result = await _scanService.ScanAsync(reqPath);
        return View(new ScanPageViewModel { Request = request, Result = result });
    }
}
