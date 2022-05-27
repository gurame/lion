using Microsoft.EntityFrameworkCore;

namespace Lion.Core.Application._Common.Models;

public class PagedList<T>
{
    public List<T> Items { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;

    private PagedList(List<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = new();
        Items.AddRange(items);

        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }

    public static class Factory
    {
        public static async Task<PagedList<T>> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalItems = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, totalItems, pageNumber, pageSize);
        }
    }
}