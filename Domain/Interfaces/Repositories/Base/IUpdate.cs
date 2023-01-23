namespace Domain.Interfaces.Repositories.Base;

public interface IUpdate<T> where T : class
{
    Task<T> UpdateAsync(T entity);
}
