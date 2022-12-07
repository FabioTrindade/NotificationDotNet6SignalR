using NotificationDotNet6SignalR.Domain.Abstracts;

namespace NotificationDotNet6SignalR.Domain.Entities;

public record Notification : Entity
{
    //Constructor
    public Notification()
    {
    }

    public Notification(Guid fromUserId,
        Guid? toUserId,
        string header,
        string content,
        bool isRead)
    {
        FromUserId = fromUserId;
        ToUserId = toUserId;
        Header = header;
        Content = content;
        IsRead = isRead;
    }


    // Properties
    public string Header { get; private set; }

	public string Content { get; private set; }

    public bool IsRead { get; private set; }


    // Modifier
    public void SetIdRead(bool isRead)
    {
        this.IsRead = isRead;
    }


    // Relationship
    public Guid FromUserId { get; private set; }

    public Guid? ToUserId { get; private set; }

    public virtual User User { get; private set; }
}