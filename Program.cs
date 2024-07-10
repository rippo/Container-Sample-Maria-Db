using System.Data;
using System.Data.Common;
using Dapper;
using MySqlConnector;

namespace Container;

class Program
{
    static async Task Main(string[] args)
    {
        using var connection = ConnectionFactory.GetOpenConnection();
        
        var data = await connection.QueryAsync("select * from tmo;");
        foreach (var row in data)
        {
            Console.WriteLine($"Id: {row.Id}");
        }
    }
}

public class ConnectionFactory
{
    public static IDbConnection GetOpenConnection()
    {
        var connection = (DbConnection)new MySqlConnection(ApplicationSettings.DbConnectionString);
        connection.Open();

        return connection;
    }
}

public static class ApplicationSettings
{
    public static string DbConnectionString => "Data Source=127.0.0.1;Initial Catalog=test;UID=user;PWD=password;";
}

