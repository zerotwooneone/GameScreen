using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GameScreen.Navigation;
using GameScreen.Node;
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
        private string _name;
        private string _id;
        public ICommand LoadedCommand { get; }

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
            LocationListItemViewmodel.Factory locationListItemViewmodelFactory):this()
        {
            _location = location;
            _nodeService = nodeService;
            _nodeNavigationService = nodeNavigationService;
            _navigationContext = navigationContext;
            _locationListItemViewmodelFactory = locationListItemViewmodelFactory;

            _name = location.Name;
            _id = location.Id.ToString();

            LoadedCommand = new ActionCommand(OnLoaded);
            Locations = new PageableObservableCollection<LocationListItemViewmodel>();
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
