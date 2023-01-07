using NotificationDotNet6SignalR.Domain.Entities;

namespace NotificationDotNet6SignalR.Domain.Providers;

public interface IUserConnectionManagerProvider
{
    Task<User> LogCurrentUser();

    Task<ConnectionInfo> ConnectionCurrentUser();

    Task<HttpContext> GetContext();
}