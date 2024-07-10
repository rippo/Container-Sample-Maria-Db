using System.Data;
using System.Data.Common;
using MySqlConnector;

namespace Container.Infrastructure.Context;

public class DapperContext
{
    public static IDbConnection GetOpenConnection()
    {
        var connection = (DbConnection)new MySqlConnection(ApplicationSettings.DbConnectionString);
        connection.Open();

        return connection;
    }
}