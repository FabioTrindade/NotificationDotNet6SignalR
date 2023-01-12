using Microsoft.AspNetCore.SignalR;
using NotificationDotNet6SignalR.Domain.Services;

namespace NotificationDotNet6SignalR.Hubs;

public class NotificationHub : Hub
{
    private readonly IConnectionService _connectionService;

    public NotificationHub(IConnectionService connectionService)
    {
        _connectionService = connectionService;
    }

    public override Task OnConnectedAsync()
    {
        var context = Context.ConnectionId;

        _connectionService.CreateConnetion(context);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _connectionService.RemoveConnection();
        return base.OnDisconnectedAsync(exception);
    }
}