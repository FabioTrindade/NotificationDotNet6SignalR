using Microsoft.EntityFrameworkCore;
using NotificationDotNet6SignalR.Domain.Entities;
using NotificationDotNet6SignalR.Domain.Repositories;
using NotificationDotNet6SignalR.Infra.Contexts;

namespace NotificationDotNet6SignalR.Infra.Repositories;

public class ConnectionRepository : EntityRepository<Connection>, IConnectionRepository
{
    public ConnectionRepository(NotificationDotNet6SignalRDataContext context) : base(context)
    {
    }

    public async Task<IList<User>> GetUsersConnected() {
        var users = await _context.Users
                        .Include(u => u.Connections)
                        .Where(w => w.Active == true)
                        .AsNoTracking()
                        .ToListAsync();

        return users;
    }

    public async Task<User> GetConnectionsByUserId(Guid? userId)
    {
        var user = await _context.Users
                        .Include(u => u.Connections)
                        .FirstOrDefaultAsync(w => w.Id == userId);

        return user;
    }

    public async Task<IList<Connection>> GetAllConnectionsByUserId(Guid? userId)
    {
        var connections = await _context.Connections
                        .Where(w => w.UserId == userId)
                        .AsNoTracking()
                        .ToListAsync();

        return connections;
    }
}