using NotificationDotNet6SignalR.Domain.Commands;
using NotificationDotNet6SignalR.Domain.Commands.User;

namespace NotificationDotNet6SignalR.Domain.Services;

public interface IUserService : IService<UserRegisterCommand>,
    IService<UserLoginCommand>
{
    Task<GenericCommandResult> LogCurrentUser();

    void Logout();
}