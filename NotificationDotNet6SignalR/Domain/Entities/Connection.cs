using NotificationDotNet6SignalR.Domain.Abstracts;

namespace NotificationDotNet6SignalR.Domain.Entities;

public record Connection : Entity
{
    //Constructor
    public Connection()
	{
	}

    public Connection(string connectionId
        , string userAgent
        , bool connected)
    {
        ConnectionId = connectionId;
        UserAgent = userAgent;
        Connected = connected;
    }

    public string ConnectionId { get; private set; }

    public string UserAgent { get; private set; }

    public bool Connected { get; private set; }


    // Relationship
    public virtual User User { get; private set; }
}