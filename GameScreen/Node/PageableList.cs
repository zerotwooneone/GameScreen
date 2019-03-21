using System.Collections.Generic;

namespace GameScreen.Node
{
    public class PageableList<T> :List<T>, IPageable<T>
    {
        public PageableList(int pageSize, bool hasMore)
        {
            PageSize = pageSize;
            HasMore = hasMore;
        }

        public PageableList(IEnumerable<T> items, int pageSize, bool hasMore):base(items)
        {
            PageSize = pageSize;
            HasMore = hasMore;
        }

        public int PageSize { get; }
        public bool HasMore { get; }
    }
}