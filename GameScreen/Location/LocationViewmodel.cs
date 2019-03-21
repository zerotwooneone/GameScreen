using System.Runtime.InteropServices;
using GameScreen.Node;
using GameScreen.NodeHistory;
using GameScreen.NodeWindow;

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

        public LocationViewmodel(INodeHistoryAccessor nodeHistoryAccessor, 
            LocationModel location,
            INodeService nodeService,
            INodeNavigationService nodeNavigationService)
        {
            _nodeHistoryAccessor = nodeHistoryAccessor;
            _location = location;
            _nodeService = nodeService;
            _nodeNavigationService = nodeNavigationService;

            _name = location.Name;
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        //public override void OnInitialized(object sender, EventArgs e)
        //{
        //    _nodeService.GetChildLocationsByParentId()
        //}
    }
}
