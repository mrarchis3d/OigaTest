
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Infrastructure.Data;

public class DBConnection : IDbConnectionFactory
{
    private readonly IOptions<ConnectionStrings> _databaseConnection;
    public DBConnection(IOptionsSnapshot<ConnectionStrings> databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }
    public IDbConnection CreateConnection(Databases dbs = Databases.Test1)
    {
        switch (dbs)
        {
            case Databases.Test1:
                return new SqlConnection(_databaseConnection.Value.Test1);
            case Databases.Test2:
                return new SqlConnection(_databaseConnection.Value.Test2);
            default:
                return new SqlConnection(_databaseConnection.Value.Test1);
        }
    }
}
