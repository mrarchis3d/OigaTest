using Domain.Interfaces.Repositories.Base;
namespace Domain.Interfaces.Repositories;

public interface IRepository<T, Tid>: ICommands<T,Tid>, IQueries<T> where T: class
{

}
