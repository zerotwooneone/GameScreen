using System.Collections.Generic;

namespace GameScreen.Node
{
    public interface IPageable<out T> : IEnumerable<T>
    {
        int PageSize { get; }
        bool HasMore { get; }
    }
}