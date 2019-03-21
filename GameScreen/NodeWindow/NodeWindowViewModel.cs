using System.Threading.Tasks;
using System.Windows;
using GameScreen.Location;
using GameScreen.MongoDb;
using GameScreen.Navigation;
using GameScreen.Node;

namespace GameScreen.NodeWindow
{
    public class NodeWindowViewModel : ViewModelBase, INavigationContext
    {
        public delegate NodeWindowViewModel LocationFactory(LocationViewmodel locationViewModel);
        private LocationViewmodel _locationViewModel;
        private readonly INodeNavigationService _nodeNavigationService;
        private readonly ILocationService _locationService;
        private readonly LocationViewmodel.Factory _locationViewmodelFactory;

        public NodeWindowViewModel(LocationViewmodel locationViewModel,
            INodeNavigationService nodeNavigationService,
            ILocationService locationService,
            LocationViewmodel.Factory locationViewmodelFactory)
        {
            _locationViewModel = locationViewModel;
            _nodeNavigationService = nodeNavigationService;
            _locationService = locationService;
            _locationViewmodelFactory = locationViewmodelFactory;
        }

        //public override void OnInitialized(object sender, EventArgs e)
        //{
        //    IDisposable navigationSubscription = null;
        //    navigationSubscription = _nodeNavigationService
        //        .NavigationObservable
        //        .Subscribe(async navigationParam =>
        //        {
        //            if (navigationParam.NewWindow)
        //            {
        //                //ignore
        //            }
        //            else
        //            {
        //                var location = await _nodeViewmodelFactory.GetLocation(navigationParam.LocationId);
        //                Dispatcher.CurrentDispatcher.Invoke(()=>
        //                {
        //                    Location = location;
        //                });
        //                navigationSubscription.Dispose();
        //            }
        //        });
        //}

        public LocationViewmodel Location
        {
            get => _locationViewModel;
            protected set
            {
                SetProperty(ref _locationViewModel, value);
                RaisePropertyChanged(nameof(LocationVisibility));
            }
        }

        public Visibility LocationVisibility
        {
            get => Location == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public async Task GoToLocation(string locationId)
        {
            var locationModel = await _locationService.GetLocationById("5c9174b4a1effb00d8cba037");
            var locationViewModel = _locationViewmodelFactory.Invoke(locationModel);
            Location = locationViewModel;
        }
    }
}
