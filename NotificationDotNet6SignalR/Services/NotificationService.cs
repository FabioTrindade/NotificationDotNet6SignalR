using Microsoft.AspNetCore.SignalR;
using NotificationDotNet6SignalR.Domain.Commands;
using NotificationDotNet6SignalR.Domain.Commands.Notifications;
using NotificationDotNet6SignalR.Domain.Entities;
using NotificationDotNet6SignalR.Domain.Providers;
using NotificationDotNet6SignalR.Domain.Repositories;
using NotificationDotNet6SignalR.Domain.Services;
using NotificationDotNet6SignalR.Hubs;
using NotificationDotNet6SignalR.Infra.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NotificationDotNet6SignalR.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _notificationHubContext;
    private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
    private readonly IUserConnectionManagerProvider _userConnectionManagerProvider;
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserService _userService;

    public NotificationService(
        IHubContext<NotificationHub> notificationHubContext,
        IHubContext<NotificationUserHub> notificationUserHubContext,
        IUserConnectionManagerProvider userConnectionManagerProvider,
        INotificationRepository notificationRepository,
        IUserService userService)
    {
        _notificationHubContext = notificationHubContext;
        _notificationUserHubContext = notificationUserHubContext;
        _userConnectionManagerProvider = userConnectionManagerProvider;
        _notificationRepository = notificationRepository;
        _userService = userService;
    }

    public async Task<GenericCommandResult> Handle(NotificationCreateCommand command)
    {
        // Executa as validaçõoes
        command.Validate();

        // Utilizado para retornar as validações caso aponte algo
        if (!command.IsValid)
            return new GenericCommandResult(false, "", command.Notifications);

        var user = await _userService.LogCurrentUser();

        // Valida se o usuário do context existe
        if (user is null)
        {
            command.AddNotification("", "Usuário não encontrado no contexto.");
            return new GenericCommandResult(false, "", command.Notifications);
        }

        var newNotification = new Notification(
            fromId: user.Id,
            header: command.Header,
            content: command.Content
        );

        if (command.ToUserId is not null)
            newNotification.SetUserToId(command.ToUserId);

        var notification = await _notificationRepository.CreateAsync(newNotification);

        await SendNotification(notification);

        return new GenericCommandResult(true, "", notification);
    }

    public async Task<GenericCommandResult> Handle(NotificationGetCommand command)
    {
        var result = await _notificationRepository.GetAllNotifications();
        return new GenericCommandResult(true, "", result);
    }

    private async Task SendNotification(Notification notification)
    {
        if (notification.ToId is not null)
        {
            var connections = _userConnectionManagerProvider.GetUserConnections(notification.ToId.ToString());

            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {
                    await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("sendToUser", notification.Header, notification.Content);
                }
            }
        }
        else {
            await _notificationHubContext.Clients.All.SendAsync("sendToUser", notification.Header, notification.Content);
        }
    }
}