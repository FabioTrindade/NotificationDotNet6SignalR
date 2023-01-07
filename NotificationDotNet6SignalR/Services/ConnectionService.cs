using NotificationDotNet6SignalR.Domain.Entities;
using NotificationDotNet6SignalR.Domain.Providers;
using NotificationDotNet6SignalR.Domain.Repositories;
using NotificationDotNet6SignalR.Domain.Services;

namespace NotificationDotNet6SignalR.Services;

public class ConnectionService : IConnectionService
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IUserConnectionManagerProvider _userConnectionManagerProvider;

    public ConnectionService(IConnectionRepository connectionRepository
        , IUserConnectionManagerProvider userConnectionManagerProvider)
    {
        _connectionRepository = connectionRepository;
        _userConnectionManagerProvider = userConnectionManagerProvider;
    }

    public async Task CreateConnetion(string connectionId)
    {
        var user = await _userConnectionManagerProvider.LogCurrentUser();

        if (user is null)
            return;

        var connection = new Connection(
            connectionId: connectionId,
            connected: true,
            userId: user.Id
        );

        await _connectionRepository.CreateAsync(connection);
    }

    public async Task StopedConnection()
    {
        var context = await _userConnectionManagerProvider.GetContext();

        var connection = await _connectionRepository.Get(w => w.ConnectionId == context.Connection.Id);

        if (connection is null)
            return;

        connection.SetConnected(false);

        await _connectionRepository.UpdateAsync(connection);
    }

    public async Task RemoveConnection()
    {
        var user = await _userConnectionManagerProvider.LogCurrentUser();

        var userCconnections = await _connectionRepository.GetAllConnectionsByUserId(user.Id);

        if (userCconnections is null)
            return;

        await _connectionRepository.DeleteAll(userCconnections);
    }

    public async Task<IList<User>> GetUsersConnected()
    {
        var users = await _connectionRepository.GetUsersConnected();
        return users;
    }

    public async Task<User> GetConnectionsByUserId(Guid? userId)
    {
        var user = await _connectionRepository.GetConnectionsByUserId(userId);
        return user;
    }
}