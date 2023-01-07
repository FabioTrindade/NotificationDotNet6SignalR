using NotificationDotNet6SignalR.Configurations;
using NotificationDotNet6SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add Context Accessor
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Hub
builder.Services.AddSignalR();

// Initialize database
builder.Services.InitializeDatabase(builder.Configuration);

// SignIn - Unique e-mail, Password and Default settings
builder.Services.SignIn();

// Data protection token provider
builder.Services.DataProtectionTokenProviderOptions();

// Password hasher
builder.Services.PasswordHasherOptions();

// Cookie temp data provider
builder.Services.CookieTempDataProviderOptions();

// Cookie
builder.Services.Cookie();

// Depency Injection - Service, Repository and provider
builder.Services.SetConfigureScopedService();
builder.Services.SetConfigureScopedRepository();
builder.Services.SetConfigureScopedProvider();

var app = builder.Build();

// Create database and execute migrations on start project
app.Services.CreateDatabaseAndExecuteMigrations();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<NotificationUserHub>("/NotificationUser");

app.Run();