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
            throw new NotImplementedException();
        }

        public IObservable<INavigationParam> NavigationObservable => _navigationParamSubject.AsObservable();
    }
}