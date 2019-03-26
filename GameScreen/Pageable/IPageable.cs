using System.Collections.Generic;

namespace GameScreen.Pageable
{
    public interface IPageable<out T> : IEnumerable<T>
    {
        bool HasMore { get; }
    }
}