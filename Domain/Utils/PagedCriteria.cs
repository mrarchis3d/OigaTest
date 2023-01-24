using System.ComponentModel.DataAnnotations;

namespace Domain.Utils;

public class PagedCriteria
{
    [Required]
    public int PageNumber { get; set; }
    [Required]
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public string? SortDirection { get; set; }
}
