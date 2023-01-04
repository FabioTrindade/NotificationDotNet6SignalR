using System;
using NotificationDotNet6SignalR.Domain.Providers;

namespace NotificationDotNet6SignalR.Providers;

public class UserConnectionManagerProvider : IUserConnectionManagerProvider
{
	private static readonly Dictionary<string, List<string>> _userConnectionMap = new Dictionary<string, List<string>>();
	private static readonly string _userConnectionMapLocker = string.Empty;

    public void KeepUserConnection(string userId, string connectionId)
    {
        lock (_userConnectionMapLocker)
        {
            if (!_userConnectionMap.ContainsKey(userId))
            {
                _userConnectionMap[userId] = new List<string>();
            }
            _userConnectionMap[userId].Add(connectionId);
        }
    }

    public void RemoveUserConnection(string connectionId)
    {
        //This method will remove the connectionId of user 
        lock (_userConnectionMapLocker)
        {
            foreach (var userId in _userConnectionMap.Keys)
            {
                if (_userConnectionMap.ContainsKey(userId))
                {
                    if (_userConnectionMap[userId].Contains(connectionId))
                    {
                        _userConnectionMap[userId].Remove(connectionId);
                        break;
                    }
                }
            }
        }
    }

    public List<string> GetUserConnections(string userId)
    {
        var conn = new List<string>();
        lock (_userConnectionMapLocker)
        {
            conn = _userConnectionMap[userId];
        }
        return conn;
    }
}