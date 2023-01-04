using Microsoft.AspNetCore.SignalR;
using NotificationDotNet6SignalR.Domain.Entities;
using NotificationDotNet6SignalR.Domain.Providers;
using NotificationDotNet6SignalR.Domain.Services;

namespace NotificationDotNet6SignalR.Hubs;

public class NotificationUserHub : Hub
{
    
    public NotificationUserHub()
	{    
    }

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }


    //public async Task<string> GetConnectionId()
    //{
    //    var user = await _userService.LogCurrentUser();
    //    var connection = await _userService.ConnectionCurrentUser();

    //    _userConnectionManagerProvider.KeepUserConnection(user.Id.ToString(), connection.Id);

    //    return connection.Id;
    //}

    //Called when a connection with the hub is terminated.
    public override Task OnDisconnectedAsync(Exception exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}