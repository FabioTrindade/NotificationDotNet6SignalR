using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotificationDotNet6SignalR.Configurations;
using NotificationDotNet6SignalR.Hubs;
using NotificationDotNet6SignalR.Infra.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add Context Accessor
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Hub
builder.Services.AddSignalR();

// Initialize database
DataContextConfiguration.InitializeDatabase(builder);

// SignIn - Unique e-mail, Password and Default settings
IdentityConfiguration.SignIn(builder);

// Data protection token provider
IdentityConfiguration.DataProtectionTokenProviderOptions(builder);

// Password hasher
IdentityConfiguration.PasswordHasherOptions(builder);

// Cookie temp data provider
IdentityConfiguration.CookieTempDataProviderOptions(builder);

// Cookie
IdentityConfiguration.Cookie(builder);

// Depency Injection - Service, Repository and provider
DependencyInjectionConfiguration.SetConfigureScopedService(builder);
DependencyInjectionConfiguration.SetConfigureScopedRepository(builder);
DependencyInjectionConfiguration.SetConfigureScopedProvider(builder);

var app = builder.Build();

// Create database and execute migrations on start project
DataContextConfiguration.CreateDatabaseAndExecuteMigrations(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHub<NotificationHub>("/NotificationHub");
//});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<NotificationHub>("/NotificationHub");
app.MapHub<NotificationHub>("/NotificationUserHub");

app.Run();