using NotificationDotNet6SignalR.Domain.Commands.Notifications;

namespace NotificationDotNet6SignalR.Domain.Services;

public interface INotificationService : IService<NotificationCreateCommand>
{
}