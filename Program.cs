using Task_DirectoryTracker.Abstractions;
using Task_DirectoryTracker.Middlewares;
using Task_DirectoryTracker.Models.Entities;
using Task_DirectoryTracker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHashService, HashService>();
builder.Services.AddScoped<IDirectoryScanner, DirectoryScanner>();
builder.Services.AddScoped<IChangeDetector, ChangeDetector>();
builder.Services.AddScoped<ISnapshotStorage, SnapshotStorage>();
builder.Services.AddScoped<IScanService, ScanService>();

// Configure addtional settings from appsettings.json
builder.Services.Configure<SnapshotSetting>(builder.Configuration.GetSection("SnapshotSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
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
