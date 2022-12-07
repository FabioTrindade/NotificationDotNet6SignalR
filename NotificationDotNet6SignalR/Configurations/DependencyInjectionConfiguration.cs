

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
	}

	public static void SetConfigureScopedRepository(this IServiceCollection services)
	{
		services.AddScoped<IUserRepository, UserRepository>();
	}

	public static void SetConfigureScopedProvider(this IServiceCollection services)
	{
		services.AddSingleton<IUserConnectionManagerProvider, UserConnectionManagerProvider>();
	}
}