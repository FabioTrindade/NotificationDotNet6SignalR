using Microsoft.AspNetCore.Mvc.Rendering;
using NotificationDotNet6SignalR.Domain.Commands;
using NotificationDotNet6SignalR.Domain.Commands.User;
using NotificationDotNet6SignalR.Domain.Entities;

namespace NotificationDotNet6SignalR.Domain.Services;

public interface IUserService : IService<UserRegisterCommand>,
    IService<UserLoginCommand>
{
    Task<GenericCommandResult> GetUserActive();

    Task Logout();
}