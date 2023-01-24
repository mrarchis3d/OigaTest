

using Domain.Interfaces.Repositories;

namespace Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<T, Tid> GetRepository<T, Tid>() where T : class;
        IUserRepository<T, Tid> GetUserRepository<T, Tid>() where T : class;
        void SaveChanges();
    }
}
