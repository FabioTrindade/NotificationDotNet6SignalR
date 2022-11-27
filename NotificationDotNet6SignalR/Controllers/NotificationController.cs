using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificationDotNet6SignalR.Domain.Providers;
using NotificationDotNet6SignalR.Hubs;
using NotificationDotNet6SignalR.Models;

namespace NotificationDotNet6SignalR.Controllers;

public class NotificationController : Controller
{
    private readonly IHubContext<NotificationHub> _notificationHubContext;
    private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
    private readonly IUserConnectionManagerProvider _userConnectionManagerProvider;

    public NotificationController(IHubContext<NotificationHub> notificationHubContext,
        IHubContext<NotificationUserHub> notificationUserHubContext,
        IUserConnectionManagerProvider userConnectionManagerProvider)
    {
        _notificationHubContext = notificationHubContext;
        _notificationUserHubContext = notificationUserHubContext;
        _userConnectionManagerProvider = userConnectionManagerProvider;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Index(Article model)
    {
        await _notificationHubContext.Clients.All.SendAsync("sendToUser", model.Heading, model.Content);
        return View();
    }

    public IActionResult SendToSpecificUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> SendToSpecificUser(Article model)
    {
        var connections = _userConnectionManagerProvider.GetUserConnections(model.UserId);

        if (connections != null && connections.Count > 0)
        {
            foreach (var connectionId in connections)
            {
                await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("sendToUser", model.Heading, model.Content);
            }
        }

        return View();
    }
}