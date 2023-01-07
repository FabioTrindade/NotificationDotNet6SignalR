using NotificationDotNet6SignalR.Domain.Abstracts;

namespace NotificationDotNet6SignalR.Domain.Entities;

public record Connection : Entity
{
    //Constructor
    public Connection()
	{
	}

    public Connection(string connectionId
        , bool connected
        , Guid userId)
    {
        ConnectionId = connectionId;
        Connected = connected;
        UserId = userId;
    }


    // Properties
    public string ConnectionId { get; private set; }

    public bool Connected { get; private set; }


    // Modifier
    public void SetConnected(bool connected) => this.Connected = connected;


    // Relationship
    public Guid UserId { get; private set; }

    public virtual User? User { get; private set; }
}