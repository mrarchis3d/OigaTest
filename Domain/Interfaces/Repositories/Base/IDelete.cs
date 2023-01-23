namespace Domain.Interfaces.Repositories.Base;

public interface IDelete<Tid>
{
    public Task DeleteAsync(Tid id);
}
