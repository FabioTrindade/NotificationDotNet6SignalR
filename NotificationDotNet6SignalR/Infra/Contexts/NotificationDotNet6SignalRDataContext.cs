
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotificationDotNet6SignalR.Domain.Entities;

namespace NotificationDotNet6SignalR.Infra.Contexts;

public class NotificationDotNet6SignalRDataContext : IdentityDbContext<User>
{
	public NotificationDotNet6SignalRDataContext()
	{
	}

	public NotificationDotNet6SignalRDataContext(DbContextOptions options) :  base(options) { }

}