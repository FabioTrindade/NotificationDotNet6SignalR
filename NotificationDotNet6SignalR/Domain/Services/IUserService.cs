using NotificationDotNet6SignalR.Domain.Commands.User;

namespace NotificationDotNet6SignalR.Domain.Services;

public interface IUserService : IService<UserRegisterCommand>
{
    void Logout();
}