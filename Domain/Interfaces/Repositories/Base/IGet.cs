
namespace Domain.Interfaces.Repositories.Base;

public interface IGet<T, Tid> where T : class
{
    Task<T> GetByIdAsync(Tid id);
}
