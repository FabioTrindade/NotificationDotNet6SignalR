
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotificationDotNet6SignalR.Commons.Extensions;
using NotificationDotNet6SignalR.Domain.Entities;
using NotificationDotNet6SignalR.Infra.Mappings;

namespace NotificationDotNet6SignalR.Infra.Contexts;

public class NotificationDotNet6SignalRDataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    private readonly IConfiguration _configuration;

    public NotificationDotNet6SignalRDataContext()
	{
	}

	public NotificationDotNet6SignalRDataContext(DbContextOptions options) :  base(options) { }

    public DbSet<Notification> Notifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlite(_configuration.GetConnectionString("NotificationConnection"),
                x => x.MigrationsHistoryTable("NotificationMigrations"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddConfiguration(new NotificationMapping());
    }
}