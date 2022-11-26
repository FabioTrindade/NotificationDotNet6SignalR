using NotificationDotNet6SignalR.Domain.Commands.User;
using NotificationDotNet6SignalR.Domain.Services;

namespace NotificationDotNet6SignalR.Domain.Repositories;

public interface IUserRepository : IService<UserRegisterCommand>,
    IService<UserLoginCommand>
{
    void Logout();
}