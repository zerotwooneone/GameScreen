using System;

namespace GameScreen.Dispatcher
{
    public class DispatcherWrapper : IDispatcher
    {
        private readonly System.Windows.Threading.Dispatcher _currentDispatcher;

        public DispatcherWrapper(System.Windows.Threading.Dispatcher currentDispatcher)
        {
            _currentDispatcher = currentDispatcher;
        }

        /// <summary>Executes the specified <see cref="T:System.Action" /> synchronously on the thread the <see cref="T:System.Windows.Threading.Dispatcher" /> is associated with.</summary>
        /// <param name="callback">A delegate to invoke through the dispatcher.</param>
        public virtual void Invoke(Action callback)
        {
            _currentDispatcher.Invoke(callback);
        }
    }
}