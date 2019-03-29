using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GameScreen.BreadCrumb;
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
        private readonly BreadCrumbViewmodel.Factory _breadCrumbViewmodelFactory;

        public NodeWindowViewModel(LocationModel locationModel,
            INodeNavigationService nodeNavigationService,
            ILocationService locationService,
            LocationViewmodel.Factory locationViewmodelFactory,
            DispatcherAccessor dispatcherAccessor,
            NodeHistoryState nodeHistoryState,
            BreadCrumbViewmodel.Factory breadCrumbViewmodelFactory)
        {
            _locationViewModel = locationViewmodelFactory(locationModel, this);
            _nodeNavigationService = nodeNavigationService;
            _locationService = locationService;
            _locationViewmodelFactory = locationViewmodelFactory;
            _dispatcherAccessor = dispatcherAccessor;

            _title = locationModel.Name;

            _nodeHistoryState = nodeHistoryState;
            _breadCrumbViewmodelFactory = breadCrumbViewmodelFactory;

            BackCommand = new AwaitableDelegateCommand(GoBack, ()=> BackAvailable);
            ForwardCommand = new AwaitableDelegateCommand(GoForward, ()=> ForwardAvailable);
            var breadCrumbs = ConvertToBreadCrumbs(_nodeHistoryState);
            BreadCrumbs = new ObservableCollection<BreadCrumbViewmodel>(breadCrumbs);
        }

        private IEnumerable<BreadCrumbViewmodel> ConvertToBreadCrumbs(NodeHistoryState nodeHistoryState)
        {
            foreach (var historyNode in nodeHistoryState.BackNodes)
            {
                async void OnClicked()
                {
                    var newHistoryState = nodeHistoryState.GoBackTo(historyNode);
                    var locationModel = await _locationService.GetLocationById(historyNode.LocationId);
                    await GoToLocation(locationModel, newHistoryState);
                }

                yield return _breadCrumbViewmodelFactory(historyNode.NodeName, OnClicked);
            }
        }

        private async Task GoToLocation(LocationModel locationModel, NodeHistoryState newHistoryState)
        {
            //var locationModel = await _locationService.GetLocationById(locationId);
            var locationViewModel = _locationViewmodelFactory.Invoke(locationModel, this);
            Location = locationViewModel;

            //this is a dirty hack
            Location
                .LoadedCommand
                .Execute(null);

            Title = locationModel.Name;

            _nodeHistoryState = newHistoryState;

            UpdateBreadkCrumbs();

            ((IRaiseCanExecuteChanged)BackCommand).RaiseCanExecuteChanged();
            ((IRaiseCanExecuteChanged)ForwardCommand).RaiseCanExecuteChanged();
        }

        private void UpdateBreadkCrumbs()
        {
            BreadCrumbs.Clear();
            var breakCrumbs = ConvertToBreadCrumbs(_nodeHistoryState);
            foreach (var breadCrumbViewmodel in breakCrumbs)
            {
                BreadCrumbs.Add(breadCrumbViewmodel);
            }
        }

        public LocationViewmodel Location
        {
            get => _locationViewModel;
            protected set => SetProperty(ref _locationViewModel, value);
        }

        public async Task GoToLocation(string locationId)
        {
            var locationModel = await _locationService.GetLocationById(locationId);
            //var locationViewModel = _locationViewmodelFactory.Invoke(locationModel, this);
            //Location = locationViewModel;
            //Title = locationModel.Name;

            var historyNode = new HistoryNode(locationModel.Name, locationId);
            var newHistoryState = _nodeHistoryState.Append(historyNode);

            //((IRaiseCanExecuteChanged)BackCommand).RaiseCanExecuteChanged();
            //((IRaiseCanExecuteChanged)ForwardCommand).RaiseCanExecuteChanged();
            await GoToLocation(locationModel, newHistoryState);
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

        public bool ForwardAvailable { get; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ObservableCollection<BreadCrumbViewmodel> BreadCrumbs { get; }
    }
}
