namespace GameScreen.Dispatcher
{
    public class DispatcherAccessor
    {
        public virtual IDispatcher Get()
        {
            return new DispatcherWrapper(System.Windows.Threading.Dispatcher.CurrentDispatcher);
        }
    }
}
