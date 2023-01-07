using NotificationDotNet6SignalR.Domain.Abstracts;

namespace NotificationDotNet6SignalR.Domain.Entities;

public record Notification : Entity
{
    //Constructor
    public Notification()
    {
    }

    public Notification(
        Guid fromId,
        string? header,
        string? content)
    {
        FromId = fromId;
        Header = header;
        Content = content;
        IsRead = false;
    }


    // Properties
    public string? Header { get; private set; }

	public string? Content { get; private set; }

    public bool IsRead { get; private set; }

    public Guid FromId { get; private set; }

    public Guid? ToId { get; private set; }


    // Modifier
    public void SetIsRead(bool isRead) => this.IsRead = isRead;

    public void SetUserToId(Guid? toId) => this.ToId = toId;


    // Relationship
    public virtual User? From { get; private set; }

    public virtual User? To { get; private set; }
}