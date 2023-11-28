using Microsoft.EntityFrameworkCore;

namespace SpaceTrading.Production.Domain.Features
{
    public class PagedList<T>
    {
        private const int MaxPageSize = 1000;

        public PagedList(List<T> items, int count, int pageNumber = 1, int pageSize = 25)
        {
            Items = new List<T>(items);
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
        }

        public List<T> Items { get; set; }
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        public static async Task<PagedList<T>> ToPagedList(IQueryable<T> source, PageParameters pageParameters)
        {
            var pageSize = Math.Min(MaxPageSize, pageParameters.PageSize);
            var count = source.Count();
            var items = await source.Skip((pageParameters.Page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageParameters.Page, pageSize);
        }
    }
}