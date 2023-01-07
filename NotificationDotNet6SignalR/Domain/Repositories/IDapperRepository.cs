using System;
namespace NotificationDotNet6SignalR.Domain.Repositories;

public interface IDapperRepository
{
    Task<List<T>> QueryAsync<T>(string query, object parameter = null!);
}