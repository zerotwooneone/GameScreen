using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GameScreen.Dispatcher;
using GameScreen.Location;
using GameScreen.Navigation;
using GameScreen.Node;
using Microsoft.Expression.Interactivity.Core;

namespace GameScreen.NodeWindow
{
    public class NodeWindowViewModel : ViewModelBase, INavigationContext
    {
        public delegate NodeWindowViewModel LocationFactory(LocationViewmodel locationViewModel);
        private LocationViewmodel _locationViewModel;
        private readonly INodeNavigationService _nodeNavigationService;
        private readonly ILocationService _locationService;
        private readonly LocationViewmodel.Factory _locationViewmodelFactory;
        private readonly DispatcherAccessor _dispatcherAccessor;
        public override ICommand LoadedCommand { get; }

        public NodeWindowViewModel(LocationViewmodel locationViewModel,
            INodeNavigationService nodeNavigationService,
            ILocationService locationService,
            LocationViewmodel.Factory locationViewmodelFactory,
            DispatcherAccessor dispatcherAccessor)
        {
            _locationViewModel = locationViewModel;
            _nodeNavigationService = nodeNavigationService;
            _locationService = locationService;
            _locationViewmodelFactory = locationViewmodelFactory;
            _dispatcherAccessor = dispatcherAccessor;
            LoadedCommand = new ActionCommand(OnLoaded);
        }

        private void OnLoaded()
        {
            IDisposable navigationSubscription = null;
            navigationSubscription = _nodeNavigationService
                .NavigationObservable
                .Subscribe(async navigationParam =>
                {
                    if (navigationParam.NewWindow)
                    {
                        //ignore
                    }
                    else
                    {
                        var location = await _locationService.GetLocationById(navigationParam.LocationId);
                        _dispatcherAccessor.Get().Invoke(() =>
                        {
                            Location = _locationViewmodelFactory.Invoke(location);
                        });
                    }
                });
        }

        public LocationViewmodel Location
        {
            get => _locationViewModel;
            protected set => SetProperty(ref _locationViewModel, value);
        }

        public async Task GoToLocation(string locationId)
        {
            var locationModel = await _locationService.GetLocationById("5c9174b4a1effb00d8cba037");
            var locationViewModel = _locationViewmodelFactory.Invoke(locationModel);
            Location = locationViewModel;
        }
    }
}
