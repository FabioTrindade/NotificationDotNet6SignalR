using NotificationDotNet6SignalR.Domain.Commands;
using NotificationDotNet6SignalR.Domain.Commands.Notifications;

namespace NotificationDotNet6SignalR.Domain.Services;

public interface INotificationService : IService<NotificationGetCommand>
    , IService<NotificationCreateCommand>
    , IService<NotificatonGetByIdCommand>
    , IService<NotificationUpdateReadCommand>
{
    Task<GenericCommandResult> GetNotificationByUserId();
}