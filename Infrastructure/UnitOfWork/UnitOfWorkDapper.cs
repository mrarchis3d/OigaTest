using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.Repositories;
using System.Data;

namespace Infrastructure.UnitOfWork;

/// <summary>
/// You need to choose Only One UnitOfWork, It's not possible register two different in your startup file
/// </summary>
public class UnitOfWorkDapper : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private readonly IDbTransaction _transaction;
    private bool _disposed;

    public IRepository<T, Tid> GetRepository<T, Tid>() where T : class
    {
        return new DapperRepository<T, Tid>(_connection, _transaction);
    }

    public UnitOfWorkDapper(IDbConnectionFactory dbFactory, Databases dbs = Databases.Test1)
    {
        _connection = dbFactory.CreateConnection(dbs);
        _connection.Open();
        _transaction = _connection.BeginTransaction();
    }

    public void SaveChanges()
    {
        try
        {
            _transaction.Commit();
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
        finally
        {
            _transaction.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                }
                if (_connection != null)
                {
                    _connection.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
