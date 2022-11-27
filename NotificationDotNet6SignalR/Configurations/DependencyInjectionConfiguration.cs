

using NotificationDotNet6SignalR.Domain.Providers;
using NotificationDotNet6SignalR.Domain.Repositories;
using NotificationDotNet6SignalR.Domain.Services;
using NotificationDotNet6SignalR.Infra.Repositories;
using NotificationDotNet6SignalR.Services;
using NotificationDotNet6SignalR.Providers;

namespace NotificationDotNet6SignalR.Configurations;

public class DependencyInjectionConfiguration
{
	public static void SetConfigureScopedService(WebApplicationBuilder builder)
	{
		builder.Services.AddTransient<IUserService, UserService>();
	}

	public static void SetConfigureScopedRepository(WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<IUserRepository, UserRepository>();
	}

	public static void SetConfigureScopedProvider(WebApplicationBuilder builder)
	{
		builder.Services.AddSingleton<IUserConnectionManagerProvider, UserConnectionManagerProvider>();
	}
}