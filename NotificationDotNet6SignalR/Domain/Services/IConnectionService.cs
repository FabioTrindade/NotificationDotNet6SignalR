using NotificationDotNet6SignalR.Domain.Entities;

namespace NotificationDotNet6SignalR.Domain.Services;

public interface IConnectionService
{
    Task CreateConnetion(string connectionId);

    Task StopedConnection();

    Task RemoveConnection();

    Task<IList<User>> GetUsersConnected();

    Task<User> GetConnectionsByUserId(Guid? userId);
}