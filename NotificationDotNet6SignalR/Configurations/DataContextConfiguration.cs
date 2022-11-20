using Microsoft.EntityFrameworkCore;
using NotificationDotNet6SignalR.Infra.Contexts;

namespace NotificationDotNet6SignalR.Configurations;

public static class DataContextConfiguration
{
    public static void InitializeDatabase(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<NotificationDotNet6SignalRDataContext>(options
           => options.UseSqlite(builder.Configuration.GetConnectionString("NotificationConnection"),
           m => m.MigrationsHistoryTable("NotificationMigrations")));
    }

    public static void CreateDatabaseAndExecuteMigrations(WebApplication app)
    {
        var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        scope.ServiceProvider.GetRequiredService<NotificationDotNet6SignalRDataContext>().Database.Migrate();
    }
}