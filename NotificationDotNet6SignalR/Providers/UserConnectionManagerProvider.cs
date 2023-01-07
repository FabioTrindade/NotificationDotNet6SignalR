using Microsoft.AspNetCore.Identity;
using NotificationDotNet6SignalR.Domain.Entities;
using NotificationDotNet6SignalR.Domain.Providers;

namespace NotificationDotNet6SignalR.Providers;

public class UserConnectionManagerProvider : IUserConnectionManagerProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;

    public UserConnectionManagerProvider(IHttpContextAccessor httpContextAccessor
        , UserManager<User> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<User> LogCurrentUser()
    {
        var username = _httpContextAccessor?.HttpContext?.User;

        var user = await _userManager.GetUserAsync(username);

        return user;
    }

    public async Task<ConnectionInfo> ConnectionCurrentUser()
        => _httpContextAccessor?.HttpContext?.Connection;

    public async Task<HttpContext> GetContext()
        => _httpContextAccessor?.HttpContext;
}