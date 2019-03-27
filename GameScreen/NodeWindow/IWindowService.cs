using System.Threading.Tasks;

namespace GameScreen.NodeWindow
{
    public interface IWindowService
    {
        Task OpenNewNode(NodeWindowViewModel nodeWindowViewModel);
    }
}