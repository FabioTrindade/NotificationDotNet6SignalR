using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using NotificationDotNet6SignalR.Domain.Repositories;

namespace NotificationDotNet6SignalR.Infra.Repositories;

public class DapperRepository : IDapperRepository
{
	protected readonly SqliteConnection _connection;
    private readonly IConfiguration _configuration;

    public DapperRepository()
    {
        _connection = new SqliteConnection(_configuration.GetConnectionString("NotificationConnection"));
    }

    public async Task<List<T>> QueryAsync<T>(string query, object parameter = null!)
    {
        var list = Activator.CreateInstance<List<T>>();

        try
        {
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();

            var result = await _connection.QueryAsync<T>(query, parameter);
            if (result != null)
                list.AddRange(result);
        }
        finally
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
        return list.ToList();
    }

}