using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotificationDotNet6SignalR.Domain.Entities;
using NotificationDotNet6SignalR.Infra.Contexts;

namespace NotificationDotNet6SignalR.Configurations;

public static class IdentityConfiguration
{
    public static void SignIn(WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;

            // User
            options.User.AllowedUserNameCharacters = string.Empty;
            options.User.RequireUniqueEmail = true;

            // Password
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;

            // Default Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        })
            .AddEntityFrameworkStores<NotificationDotNet6SignalRDataContext>()
            .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);
    }
    public static void DataProtectionTokenProviderOptions(WebApplicationBuilder builder) {
        builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromMinutes(15));
    }   

    public static void PasswordHasherOptions(WebApplicationBuilder builder)
    {
        builder.Services.Configure<PasswordHasherOptions>(options =>
            options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2);
    }

    public static void CookieTempDataProviderOptions(WebApplicationBuilder builder)
    {
        builder.Services.Configure<CookieTempDataProviderOptions>(options =>
        {
            options.Cookie.IsEssential = true;
        });
    }

    public static void Cookie(WebApplicationBuilder builder)
    {
        builder.Services.ConfigureApplicationCookie(options =>
        {
            // options.LoginPath = "/Account/Login";
            // options.LogoutPath = "/Account/Logout";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
            // options.AccessDeniedPath = "/Account/Login";
        });
    }
}