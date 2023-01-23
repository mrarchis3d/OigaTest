namespace Domain.Interfaces.Repositories.Base;

public interface IPost<T> where T : class
{
    Task<T> AddAsync(T entity);
}
