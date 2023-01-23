using Domain.Interfaces.Repositories;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork;

/// <summary>
/// You need to choose Only One UnitOfWork, It's not possible register two different in your startup file
/// </summary>
public class UnitOfWorkEF<TContext> : IUnitOfWork where TContext : DbContext
{
    private readonly TContext _context;
    private bool _disposed;

    public IRepository<T, Tid> GetRepository<T, Tid>() where T: class
    {
        return new EFRepository<T, Tid>(_context);
    }

    public UnitOfWorkEF(TContext context)
    {
        _context = context;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
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
            if (disposing && _context != null)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}