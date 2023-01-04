using System;
using Flunt.Notifications;
using NotificationDotNet6SignalR.Domain.Contracts;

namespace NotificationDotNet6SignalR.Domain.Commands.Notifications;

public class NotificationUpdateReadCommand : Notifiable<Notification>, ICommand
{
	public Guid Id { get; set; }
	
	public bool Status { get; set; }
}