using System.Collections.Generic;

namespace GameScreen.Pageable
{
    public class PageableList<T> :List<T>, IPageable<T>
    {
        public PageableList(bool hasMore)
        {
            HasMore = hasMore;
        }

        public PageableList(IEnumerable<T> items, bool hasMore):base(items)
        {
            HasMore = hasMore;
        }

        public int PageSize { get; }
        public bool HasMore { get; }
    }
}