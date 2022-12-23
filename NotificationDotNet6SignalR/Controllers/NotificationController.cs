using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificationDotNet6SignalR.Domain.Commands.Notifications;
using NotificationDotNet6SignalR.Domain.DTOs.Notification;
using NotificationDotNet6SignalR.Domain.Providers;
using NotificationDotNet6SignalR.Domain.Services;
using NotificationDotNet6SignalR.Hubs;
using NotificationDotNet6SignalR.Models;

namespace NotificationDotNet6SignalR.Controllers;

public class NotificationController : Controller
{
    private readonly IHubContext<NotificationHub> _notificationHubContext;
    private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
    private readonly IUserConnectionManagerProvider _userConnectionManagerProvider;
    private readonly INotificationService _notificationService;

    public NotificationController(IHubContext<NotificationHub> notificationHubContext,
        IHubContext<NotificationUserHub> notificationUserHubContext,
        IUserConnectionManagerProvider userConnectionManagerProvider,
        INotificationService notificationService)
    {
        _notificationHubContext = notificationHubContext;
        _notificationUserHubContext = notificationUserHubContext;
        _userConnectionManagerProvider = userConnectionManagerProvider;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _notificationService.Handle(new NotificationGetCommand());
        return View(result.Data as List<NotificationDto>);
    }

    [HttpPost]
    public async Task<IActionResult> Index(NotificationGetCommand command)
    {
        var result = await _notificationService.Handle(command);
        return View(result.Data as List<NotificationDto>);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(NotificationCreateCommand command)
    {
        await _notificationService.Handle(command);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult All()
    {
        return View();
    }

    [HttpGet]
    public IActionResult SendToSpecificUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendToSpecificUser(Article model)
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

    [HttpPost]
    public async Task<IActionResult> SendToAllUser(Article model)
    {
        await _notificationHubContext.Clients.All.SendAsync("sendAllUser", model.Heading, model.Content);

        return View();
    }
}