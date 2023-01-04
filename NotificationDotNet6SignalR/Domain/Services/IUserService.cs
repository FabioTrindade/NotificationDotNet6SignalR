using Microsoft.AspNetCore.Mvc.Rendering;
using NotificationDotNet6SignalR.Domain.Commands;
using NotificationDotNet6SignalR.Domain.Commands.User;
using NotificationDotNet6SignalR.Domain.Entities;

namespace NotificationDotNet6SignalR.Domain.Services;

public interface IUserService : IService<UserRegisterCommand>,
    IService<UserLoginCommand>
{
    Task<User> LogCurrentUser();

    Task<ConnectionInfo> ConnectionCurrentUser();

    Task<GenericCommandResult> GetUserActive();

    void Logout();
}