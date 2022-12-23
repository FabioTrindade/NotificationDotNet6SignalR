using Microsoft.AspNetCore.SignalR;
using NotificationDotNet6SignalR.Domain.Providers;
using NotificationDotNet6SignalR.Domain.Services;

namespace NotificationDotNet6SignalR.Hubs;

public class NotificationUserHub : Hub
{
    private readonly IUserConnectionManagerProvider _userConnectionManagerProvider;
    private readonly IUserService _userService;

    public NotificationUserHub(IUserConnectionManagerProvider userConnectionManagerProvider,
        IUserService userService)
	{
        _userConnectionManagerProvider = userConnectionManagerProvider;
        _userService = userService;
    }

    public string GetConnectionId()
    {
        var user = _userService.LogCurrentUser().Result;
        var connection = _userService.ConnectionCurrentUser().Result;

        _userConnectionManagerProvider.KeepUserConnection(user.Id.ToString(), connection.Id);

        return connection.Id;
    }

    //Called when a connection with the hub is terminated.
    public async override Task OnDisconnectedAsync(Exception exception)
    {
        //get the connectionId
        var connection = _userService.ConnectionCurrentUser().Result;

        _userConnectionManagerProvider.RemoveUserConnection(connection.Id);

        var value = await Task.FromResult(0);
    }
}