using System.Threading.Tasks;
using System.Windows;
using GameScreen.Location;

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

        public void OpenNewLocationPopup(NewLocationViewmodel newLocationViewmodel, Window popupContext)
        {
            popupContext.DataContext = newLocationViewmodel;
            popupContext.ShowDialog();
        }

        public Window GetNewLocationPopup()
        {
            return new NewLocationWindow();
        }
    }
}