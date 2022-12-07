using System;
using NotificationDotNet6SignalR.Domain.Entities;
using NotificationDotNet6SignalR.Domain.Repositories;
using NotificationDotNet6SignalR.Infra.Contexts;

namespace NotificationDotNet6SignalR.Infra.Repositories;

public class NotificationRepository : EntityRepository<Notification>, INotificationRepository
{
	public NotificationRepository(NotificationDotNet6SignalRDataContext context) : base (context)
	{
	}
}