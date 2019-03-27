using System.Threading.Tasks;

namespace GameScreen.NodeWindow
{
    public class DataContextWindowService : IWindowService
    {
        public Task OpenNewNode(NodeWindowViewModel nodeWindowViewModel)
        {
            var window = new NodeWindow
            {
                DataContext = nodeWindowViewModel
            };

            window.Show();
            return Task.CompletedTask;
        }
    }
}