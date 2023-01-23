using Domain.Enums;
using System.Data;

namespace Domain.Interfaces.Data;

public interface IDbConnectionFactory
{
    public IDbConnection CreateConnection(Databases dbs = Databases.Test1);
}
