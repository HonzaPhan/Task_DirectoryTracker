using Task_DirectoryTracker;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices(builder);

WebApplication app = builder.Build();
app.ConfigureApplication();
