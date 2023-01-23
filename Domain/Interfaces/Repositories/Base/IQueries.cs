using Domain.Utils;

namespace Domain.Interfaces.Repositories.Base;

public interface IQueries<T>
{
    Task<PagedResult<T>> GetAsync(PagedCriteria criteria);
    Task<IEnumerable<T>> GetAsync();
}
