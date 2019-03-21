using System.Threading.Tasks;

namespace GameScreen.Navigation
{
    public interface INavigationContext
    {
        Task GoToLocation(string locationId);
    }
}