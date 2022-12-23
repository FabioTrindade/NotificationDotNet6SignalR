﻿using NotificationDotNet6SignalR.Domain.DTOs.Notification;
using NotificationDotNet6SignalR.Domain.Entities;

namespace NotificationDotNet6SignalR.Domain.Repositories;

public interface INotificationRepository : IEntityRepository<Notification>
{
    Task<List<NotificationDto>> GetAllNotifications();
}