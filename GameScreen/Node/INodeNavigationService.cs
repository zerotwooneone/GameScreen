using System;
using System.Threading.Tasks;
using GameScreen.Navigation;

namespace GameScreen.Node
{
    public interface INodeNavigationService
    {
        Task GotoLocation(string locationId);
        IObservable<INavigationParam> NavigationObservable { get; }
    }
}