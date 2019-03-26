using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GameScreen.Dispatcher;
using GameScreen.Location;
using GameScreen.Navigation;
using GameScreen.Node;
using GameScreen.NodeHistory;
using GameScreen.Viewmodel;
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

            _title = locationModel.Name;

            _nodeHistoryState = nodeHistoryState;

            BackCommand = new AwaitableDelegateCommand(GoBack, ()=>
            {
                return _nodeHistoryState.BackNodes.Any();
            });
            ForwardCommand = new AwaitableDelegateCommand(GoForward, ()=>_nodeHistoryState.ForwardNodes.Any());
        }

        public LocationViewmodel Location
        {
            get => _locationViewModel;
            protected set => SetProperty(ref _locationViewModel, value);
        }

        public async Task GoToLocation(string locationId)
        {
            var locationModel = await _locationService.GetLocationById(locationId);
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
