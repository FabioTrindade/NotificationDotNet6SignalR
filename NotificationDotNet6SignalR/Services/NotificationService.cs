using NotificationDotNet6SignalR.Commons.Extensions;
using NotificationDotNet6SignalR.Domain.Commands;
using NotificationDotNet6SignalR.Domain.Commands.Notifications;
using NotificationDotNet6SignalR.Domain.Entities;
using NotificationDotNet6SignalR.Domain.Repositories;
using NotificationDotNet6SignalR.Domain.Services;

namespace NotificationDotNet6SignalR.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserService _userService;

    public NotificationService(INotificationRepository notificationRepository,
        IUserService userService)
    {
        _notificationRepository = notificationRepository;
        _userService = userService;
    }

    public async Task<GenericCommandResult> Handle(NotificationCreateCommand command)
    {
        // Executa as validacoes
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

        var notification = new Notification(
            fromUserId: user.Id.ToGuid(),
            toUserId: command.ToUserId.ToGuid(),
            header: command.Header,
            content: command.Content,
            isRead: false
            );

        var result = await _notificationRepository.CreateAsync(notification);

        return new GenericCommandResult(true, "", result);
    }
}