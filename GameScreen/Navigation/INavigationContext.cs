using System.Threading.Tasks;

namespace GameScreen.Navigation
{
    public interface INavigationContext
    {
        Task GoToLocation(string locationId);
        Task GoBack();
        bool BackAvailable { get; }
        Task GoForward();
        bool ForwardAvailable { get; }
    }
}