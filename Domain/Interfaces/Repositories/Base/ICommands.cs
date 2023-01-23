namespace Domain.Interfaces.Repositories.Base;

public interface ICommands<T, Tid>: IGet<T, Tid>, IDelete<Tid>, IPost<T>, IUpdate<T> where T: class
{
}
