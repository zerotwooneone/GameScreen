using System;

namespace GameScreen.Dispatcher
{
    public interface IDispatcher
    {
        /// <summary>Executes the specified <see cref="T:System.Action" /> synchronously on the thread the <see cref="T:System.Windows.Threading.Dispatcher" /> is associated with.</summary>
        /// <param name="callback">A delegate to invoke through the dispatcher.</param>
        void Invoke(Action callback);
    }
}