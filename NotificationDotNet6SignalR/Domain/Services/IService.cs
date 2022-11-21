using NotificationDotNet6SignalR.Domain.Commands;

namespace NotificationDotNet6SignalR.Domain.Services;

public interface IService<T> where T : class
{
    Task<GenericCommandResult> Handle(T command);
}