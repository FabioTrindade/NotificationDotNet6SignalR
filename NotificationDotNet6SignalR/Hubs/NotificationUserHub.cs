using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using NotificationDotNet6SignalR.Domain.Providers;

namespace NotificationDotNet6SignalR.Hubs;

public class NotificationUserHub : Hub
{
    private readonly IUserConnectionManagerProvider _userConnectionManagerProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public NotificationUserHub(IUserConnectionManagerProvider userConnectionManagerProvider,
        IHttpContextAccessor httpContextAccessor)
	{
        userConnectionManagerProvider = _userConnectionManagerProvider;
        httpContextAccessor = _httpContextAccessor;
    }

    public string GetConnectionId()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var connectionId = _httpContextAccessor.HttpContext.Connection.Id;

        _userConnectionManagerProvider.KeepUserConnection(userId, connectionId);

        return connectionId;
    }

    //Called when a connection with the hub is terminated.
    public async override Task OnDisconnectedAsync(Exception exception)
    {
        //get the connectionId
        var connectionId = _httpContextAccessor.HttpContext.Connection.Id;

        _userConnectionManagerProvider.RemoveUserConnection(connectionId);

        var value = await Task.FromResult(0);
    }
}