using Microsoft.EntityFrameworkCore;
using NotificationDotNet6SignalR.Domain.DTOs.Notification;
using NotificationDotNet6SignalR.Domain.Entities;
using NotificationDotNet6SignalR.Domain.Repositories;
using NotificationDotNet6SignalR.Infra.Contexts;

namespace NotificationDotNet6SignalR.Infra.Repositories;

public class NotificationRepository : EntityRepository<Notification>, INotificationRepository
{
	public NotificationRepository(NotificationDotNet6SignalRDataContext context) : base (context)
	{
	}

    public async Task<List<NotificationDto>> GetAllNotifications()
    {
        var notifications = await _context.Notifications
            .Where(w => w.Active == true)
            .Select(s => new NotificationDto
            {
                Id = s.Id,
                FromId = s.FromId,
                FromName = string.Format("{0} {1}", s.From.FirstName, s.From.LastName),
                ToId = s.ToId,
                ToName = string.Format("{0} {1}", s.To.FirstName, s.To.LastName),
                Header = s.Header,
                Content = s.Content,
                IsRead = s.IsRead,
                CreatedAt = s.CreatedAt
            })
            .OrderByDescending(o => o.CreatedAt)
            .AsNoTracking()
            .ToListAsync();

        return notifications;
    }
}