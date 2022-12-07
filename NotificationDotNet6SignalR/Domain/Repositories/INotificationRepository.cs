using System;
using NotificationDotNet6SignalR.Domain.Entities;

namespace NotificationDotNet6SignalR.Domain.Repositories;

public interface INotificationRepository : IEntityRepository<Notification>
{
}