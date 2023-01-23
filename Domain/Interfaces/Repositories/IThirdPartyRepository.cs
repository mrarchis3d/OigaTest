using Domain.Interfaces.Repositories.Base;

namespace Domain.Interfaces.Repositories;

public interface IThirdPartyRepository
{
    Task<T> GetAsync<T>(object? obj, string command) where T : class, new();
    Task<T> Post<T>(object obj, string command);
}
