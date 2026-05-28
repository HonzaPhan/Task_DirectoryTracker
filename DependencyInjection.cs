using Task_DirectoryTracker.Abstractions;
using Task_DirectoryTracker.Middlewares;
using Task_DirectoryTracker.Models.Entities;
using Task_DirectoryTracker.Services;

namespace Task_DirectoryTracker;

/// <summary>
/// This class contains extension methods for setting up services and configuring the application. <br /> 
/// It is used to keep the Program.cs file clean and organized by separating the service registration and application configuration logic into a dedicated class.
/// </summary>
internal static class DependencyInjection
{
    /// <summary>
    /// Registers services and configurations for the app
    /// </summary>
    internal static void AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        // Add services to the container
        services.AddControllersWithViews();
        services.AddSingleton<IHashService, HashService>();
        services.AddScoped<IDirectoryScanner, DirectoryScanner>();
        services.AddScoped<IChangeDetector, ChangeDetector>();
        services.AddScoped<ISnapshotStorage, SnapshotStorage>();
        services.AddScoped<IScanService, ScanService>();

        // Configure addtional settings from appsettings.json
        services.Configure<SnapshotSetting>(builder.Configuration.GetSection("SnapshotSettings"));
    }

    /// <summary>
    /// Configures the application pipeline, including middleware and routing.
    /// </summary>
    /// <param name="app"> The WebApplication instance to configure </param>
    /// <returns> The configured WebApplication instance </returns>
    internal static WebApplication ConfigureApplication(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute(name: "default", pattern: "{controller=Scan}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();

        return app;
    }
}