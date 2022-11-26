using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificationDotNet6SignalR.Hubs;
using NotificationDotNet6SignalR.Models;

namespace NotificationDotNet6SignalR.Controllers;

public class NotificationController : Controller
{
    private readonly IHubContext<NotificationHub> _notificationHubContext;

    public NotificationController(IHubContext<NotificationHub> notificationHubContext)
    {
        _notificationHubContext = notificationHubContext;
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
}