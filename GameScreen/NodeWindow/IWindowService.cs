using System.Threading.Tasks;
using System.Windows;
using GameScreen.Location;

namespace GameScreen.NodeWindow
{
    public interface IWindowService
    {
        Task OpenNewNode(NodeWindowViewModel nodeWindowViewModel);
        void OpenNewLocationPopup(NewLocationViewmodel newLocationViewmodel, Window popupContext);
        Window GetNewLocationPopup();
    }
}