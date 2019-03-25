using System.Threading.Tasks;
using System.Windows.Input;

namespace GameScreen.WpfCommand
{
    public interface IAsyncCommand<in T> : IRaiseCanExecuteChanged
    {
        Task ExecuteAsync(T obj);
        bool CanExecute(object obj);
        ICommand Command { get; }
    }

    public interface IAsyncCommand : IAsyncCommand<object>
    {
    }
}