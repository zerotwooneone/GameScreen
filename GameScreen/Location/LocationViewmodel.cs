using System.Windows.Input;
using GameScreen.Node;
using GameScreen.NodeHistory;
using GameScreen.NodeWindow;
using Microsoft.Expression.Interactivity.Core;

namespace GameScreen.Location
{
    public class LocationViewmodel: ViewModelBase
    {
        public delegate LocationViewmodel Factory(LocationModel location);
        private readonly INodeHistoryAccessor _nodeHistoryAccessor;
        private readonly LocationModel _location;
        private readonly INodeService _nodeService;
        private readonly INodeNavigationService _nodeNavigationService;
        private string _name;
        private string _id;
        public override ICommand LoadedCommand { get; }

        /// <summary>
        /// Only for unit-testing
        /// </summary>
        protected LocationViewmodel()
        {
        }

        public LocationViewmodel(INodeHistoryAccessor nodeHistoryAccessor, 
            LocationModel location,
            INodeService nodeService,
            INodeNavigationService nodeNavigationService):this()
        {
            _nodeHistoryAccessor = nodeHistoryAccessor;
            _location = location;
            _nodeService = nodeService;
            _nodeNavigationService = nodeNavigationService;

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
