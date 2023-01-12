using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationDotNet6SignalR.Domain.Commands.Notifications;
using NotificationDotNet6SignalR.Domain.DTOs.Notification;
using NotificationDotNet6SignalR.Domain.Providers;
using NotificationDotNet6SignalR.Domain.Services;

namespace NotificationDotNet6SignalR.Controllers;

[Authorize]
public class NotificationController : Controller
{
    private readonly IUserConnectionManagerProvider _userConnectionManagerProvider;
    private readonly INotificationService _notificationService;

    public NotificationController(IUserConnectionManagerProvider userConnectionManagerProvider
        , INotificationService notificationService)
    {
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
    public async Task<IActionResult> All()
    {
        var result = await _notificationService.GetNotificationByUserId();
        return Json(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid Id) {
        var result = await _notificationService.Handle(new NotificatonGetByIdCommand(Id));
        return View(result.Data as NotificationDto);
    }

    [HttpPut]
    public async Task<IActionResult> ChangeRead(NotificationUpdateReadCommand command)
    {
        var result = await _notificationService.Handle(command);
        return Json(result);
    }
}