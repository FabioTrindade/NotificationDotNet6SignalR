using NotificationDotNet6SignalR.Domain.Entities;
using NotificationDotNet6SignalR.Domain.Repositories;
using NotificationDotNet6SignalR.Infra.Contexts;

namespace NotificationDotNet6SignalR.Infra.Repositories;

public class ConnectionRepository : EntityRepository<Connection>, IConnectionRepository
{
    public ConnectionRepository(NotificationDotNet6SignalRDataContext context) : base(context)
    {
    }
}