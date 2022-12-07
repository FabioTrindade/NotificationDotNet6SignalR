using Microsoft.EntityFrameworkCore;
using NotificationDotNet6SignalR.Infra.Contexts;

namespace NotificationDotNet6SignalR.Configurations;

public static class DataContextConfiguration
{
    public static void InitializeDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NotificationDotNet6SignalRDataContext>(options
           => options.UseSqlite(configuration.GetConnectionString("NotificationConnection"),
           m => m.MigrationsHistoryTable("NotificationMigrations")));
    }

    public static void CreateDatabaseAndExecuteMigrations(this IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        scope.ServiceProvider.GetRequiredService<NotificationDotNet6SignalRDataContext>().Database.Migrate();
    }
}