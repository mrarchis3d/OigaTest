using Domain.DTOs.User;
using Domain.Utils;
namespace Domain.Interfaces.Repositories;

public interface IUserRepository<T, Tid> : IRepository<T, Tid> where T : class
{
    Task<PagedResult<T>> GetFilteredUsers(UserPaged criteria);
}
