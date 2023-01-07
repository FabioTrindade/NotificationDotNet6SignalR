using NotificationDotNet6SignalR.Domain.Providers;
using NotificationDotNet6SignalR.Domain.Repositories;
using NotificationDotNet6SignalR.Domain.Services;
using NotificationDotNet6SignalR.Infra.Repositories;
using NotificationDotNet6SignalR.Providers;
using NotificationDotNet6SignalR.Services;

namespace NotificationDotNet6SignalR.Configurations;

public static class DependencyInjectionConfiguration
{
	public static void SetConfigureScopedService(this IServiceCollection services)
    {
		services.AddTransient<IUserService, UserService>();
		services.AddScoped<INotificationService, NotificationService>();
		services.AddScoped<IConnectionService, ConnectionService>();
    }

	public static void SetConfigureScopedRepository(this IServiceCollection services)
	{
		services.AddScoped<IDapperRepository, DapperRepository>();
		services.AddScoped<INotificationRepository, NotificationRepository>();
		services.AddScoped<IConnectionRepository, ConnectionRepository>();
    }

	public static void SetConfigureScopedProvider(this IServiceCollection services)
	{
		services.AddTransient<IUserConnectionManagerProvider, UserConnectionManagerProvider>();
	}
}