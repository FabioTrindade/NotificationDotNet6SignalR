using NotificationDotNet6SignalR.Domain.Contracts;

namespace NotificationDotNet6SignalR.Domain.Commands.Notifications;

public record NotificatonGetByIdCommand : ICommand
{
    public NotificatonGetByIdCommand(Guid notificationId)
    {
        NotificationId = notificationId;
    }

    public Guid NotificationId { get; private set; }
}