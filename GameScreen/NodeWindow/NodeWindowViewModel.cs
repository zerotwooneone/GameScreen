using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GameScreen.Dispatcher;
using GameScreen.Location;
using GameScreen.Navigation;
using GameScreen.Node;
using GameScreen.NodeHistory;
using GameScreen.WpfCommand;

namespace GameScreen.NodeWindow
{
    public class NodeWindowViewModel : ViewModelBase, INavigationContext
    {
        public delegate NodeWindowViewModel LocationFactory(LocationModel locationModel,
            NodeHistoryState nodeHistoryState);
        private LocationViewmodel _locationViewModel;
        private readonly INodeNavigationService _nodeNavigationService;
        private readonly ILocationService _locationService;
        private readonly LocationViewmodel.Factory _locationViewmodelFactory;
        private readonly DispatcherAccessor _dispatcherAccessor;
        private string _title;
        public ICommand BackCommand { get; }
        public ICommand ForwardCommand { get; }

        private NodeHistoryState _nodeHistoryState;
        //public override ICommand LoadedCommand { get; }

        public NodeWindowViewModel(LocationModel locationModel,
            INodeNavigationService nodeNavigationService,
            ILocationService locationService,
            LocationViewmodel.Factory locationViewmodelFactory,
            DispatcherAccessor dispatcherAccessor,
            NodeHistoryState nodeHistoryState)
        {
            _locationViewModel = locationViewmodelFactory(locationModel, this);
            _nodeNavigationService = nodeNavigationService;
            _locationService = locationService;
            _locationViewmodelFactory = locationViewmodelFactory;
            _dispatcherAccessor = dispatcherAccessor;
            //LoadedCommand = new ActionCommand(OnLoaded);

            _title = locationModel.Name;

            _nodeHistoryState = nodeHistoryState;

            BackCommand = new AwaitableDelegateCommand(GoBack, ()=>
            {
                return _nodeHistoryState.BackNodes.Any();
            });
            ForwardCommand = new AwaitableDelegateCommand(GoForward, ()=>_nodeHistoryState.ForwardNodes.Any());
        }

        //private void OnLoaded()
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
        //                var location = await _locationService.GetLocationById(navigationParam.LocationId);
        //                _dispatcherAccessor.Get().Invoke(() =>
        //                {
        //                    Location = _locationViewmodelFactory.Invoke(location);
        //                    Title = Location.Name;
        //                });
        //            }
        //        });
        //}

        public LocationViewmodel Location
        {
            get => _locationViewModel;
            protected set => SetProperty(ref _locationViewModel, value);
        }

        public async Task GoToLocation(string locationId)
        {
            var locationModel = await _locationService.GetLocationById("5c9174b4a1effb00d8cba037");
            var locationViewModel = _locationViewmodelFactory.Invoke(locationModel, this);
            Location = locationViewModel;
            Title = locationModel.Name;
        }

        public async Task GoBack()
        {
            _nodeHistoryState = _nodeHistoryState.GoBack();
            await GoToLocation(_nodeHistoryState.Current.LocationId);
        }

        public bool BackAvailable => _nodeHistoryState.BackNodes.Any();
        public async Task GoForward()
        {
            throw new NotImplementedException();
        }

        bool INavigationContext.ForwardAvailable { get; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
