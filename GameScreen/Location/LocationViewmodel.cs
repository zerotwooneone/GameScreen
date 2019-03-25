using System.Windows.Input;
using GameScreen.Navigation;
using GameScreen.Node;
using GameScreen.NodeWindow;
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
            INavigationContext navigationContext):this()
        {
            _location = location;
            _nodeService = nodeService;
            _nodeNavigationService = nodeNavigationService;
            _navigationContext = navigationContext;

            _name = location.Name;
            _id = location.Id.ToString();

            LoadedCommand = new ActionCommand(OnLoaded);
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

        private async void OnLoaded()
        {
            var x = await _nodeService.GetChildLocationsByParentId(Id, 10, 0);
            int y = 9;
        }
    }
}
