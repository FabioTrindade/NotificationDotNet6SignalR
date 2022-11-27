namespace NotificationDotNet6SignalR.Domain.Providers;

public interface IUserConnectionManagerProvider
{
    void KeepUserConnection(string userId, string connectionId);

    void RemoveUserConnection(string connectionId);

    List<string> GetUserConnections(string userId);
}