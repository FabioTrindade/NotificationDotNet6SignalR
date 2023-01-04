namespace NotificationDotNet6SignalR.Domain.DTOs.Notification;

public record NotificationDto
{
	public Guid Id { get; set; }

	public Guid FromId { get; set; }

	public string? FromName { get; set; }

	public Guid? ToId { get; set; }

	public string? ToName { get; set; }

	public string? Header { get; set; }

	public string? Content { get; set; }

	public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

	public string? Url => string.Format("/Notification/Details/{0}", Id);
}