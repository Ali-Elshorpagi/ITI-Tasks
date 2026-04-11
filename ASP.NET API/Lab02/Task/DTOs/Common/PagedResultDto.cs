namespace Task01.DTOs.Common;

public class PagedResultDto<T>
{
    public ICollection<T> Items { get; set; } = [];
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}