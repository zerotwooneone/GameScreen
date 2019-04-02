using System.Linq;
using System.Windows.Input;
using GameScreen.Navigation;
using GameScreen.Node;
using GameScreen.NodeWindow;
using GameScreen.Pageable;
using GameScreen.Viewmodel;
using Microsoft.Expression.Interactivity.Core;

namespace GameScreen.Location
{
    public class LocationViewmodel: ViewModelBase
    {
        public delegate LocationViewmodel Factory(LocationModel location, INavigationContext navigationContext);
        private readonly LocationModel _location;
        private readonly INodeService _nodeService;
        private readonly INodeNavigationService _nodeNavigationService;
        private readonly INavigationContext _navigationContext;
        private readonly LocationListItemViewmodel.Factory _locationListItemViewmodelFactory;
        private readonly IWindowService _windowService;
        private readonly NewLocationViewmodel.Factory _newLocationViewmodelFactory;
        private string _name;
        private string _id;
        public ICommand LoadedCommand { get; }
        public ICommand AddLocationCommand { get; }

        /// <summary>
        /// Only for unit-testing
        /// </summary>
        protected LocationViewmodel()
        {
        }

        public LocationViewmodel(LocationModel location,
            INodeService nodeService,
            INodeNavigationService nodeNavigationService,
            INavigationContext navigationContext,
            LocationListItemViewmodel.Factory locationListItemViewmodelFactory,
            IWindowService windowService,
            NewLocationViewmodel.Factory newLocationViewmodelFactory):this()
        {
            _location = location;
            _nodeService = nodeService;
            _nodeNavigationService = nodeNavigationService;
            _navigationContext = navigationContext;
            _locationListItemViewmodelFactory = locationListItemViewmodelFactory;
            _windowService = windowService;
            _newLocationViewmodelFactory = newLocationViewmodelFactory;

            _name = location.Name;
            _id = location.Id.ToString();

            LoadedCommand = new ActionCommand(OnLoaded);
            Locations = new PageableObservableCollection<LocationListItemViewmodel>();
            AddLocationCommand = new ActionCommand(OnNewLocationClick);
        }

        private void OnNewLocationClick()
        {
            var popupContext = _windowService.GetNewLocationPopup();
            var relatedLocationVm = new NewRelatedLocationViewmodel(Id, Name);
            var nlvm = _newLocationViewmodelFactory(new[] {relatedLocationVm}, () => popupContext.Close());
            _windowService.OpenNewLocationPopup(nlvm, popupContext);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Id
        {
            get => _id;
            protected set => SetProperty(ref _id, value);
        }

        public PageableObservableCollection<LocationListItemViewmodel> Locations { get; }

        private async void OnLoaded()
        {
            var pageable = await _nodeService.GetChildLocationsByParentId(Id, 10, 0);
            
            var viewModels = pageable.Select(lm =>
            {
                void SwithTo()
                {
                    _navigationContext.GoToLocation(lm.Id.ToString());
                }
                void OpenNew()
                {
                    _nodeNavigationService.GotoLocation(lm.Id.ToString());
                }
                return _locationListItemViewmodelFactory(lm.Name, SwithTo, OpenNew);
            });
            var pageableViewmodels = new PageableList<LocationListItemViewmodel>(viewModels, pageable.HasMore);
            Locations.Add(pageableViewmodels);
        }
    }
}
