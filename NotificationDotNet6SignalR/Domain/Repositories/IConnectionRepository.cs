using NotificationDotNet6SignalR.Domain.Entities;

namespace NotificationDotNet6SignalR.Domain.Repositories;

public interface IConnectionRepository : IEntityRepository<Connection>
{
    Task<IList<User>> GetUsersConnected();

    Task<User> GetConnectionsByUserId(Guid? userId);

    Task<IList<Connection>> GetAllConnectionsByUserId(Guid? userId);
}