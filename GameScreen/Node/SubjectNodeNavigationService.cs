using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using GameScreen.Navigation;

namespace GameScreen.Node
{
    public class SubjectNodeNavigationService : INodeNavigationService
    {
        private readonly ISubject<INavigationParam> _navigationParamSubject;

        public SubjectNodeNavigationService(ISubject<INavigationParam> navigationParamSubject)
        {
            _navigationParamSubject = navigationParamSubject;
        }
        public Task GotoLocation(string locationId)
        {
            _navigationParamSubject.OnNext(new NavigationParam(locationId, false));
            return Task.CompletedTask;
        }

        public IObservable<INavigationParam> NavigationObservable => _navigationParamSubject.AsObservable();
    }
}