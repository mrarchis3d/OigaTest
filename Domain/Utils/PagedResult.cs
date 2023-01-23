namespace Domain.Utils;

public class PagedResult<T>
{
    public IEnumerable<T>? Results { get; set; }
    public int Total { get; set; }
}
