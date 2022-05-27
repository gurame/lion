namespace Lion.Core.Application._Common.Models;

public class BaseQueryParams
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string OrderBy { get; set; }
}
