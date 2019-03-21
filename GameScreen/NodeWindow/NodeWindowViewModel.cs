using System.Windows;
using GameScreen.Location;
using GameScreen.Node;

namespace GameScreen.NodeWindow
{
    public class NodeWindowViewModel : ViewModelBase
    {
        public delegate NodeWindowViewModel LocationFactory(LocationViewmodel locationViewModel);
        private LocationViewmodel _locationViewModel;
        private readonly INodeNavigationService _nodeNavigationService;

        public NodeWindowViewModel(LocationViewmodel locationViewModel,
            INodeNavigationService nodeNavigationService)
        {
            _locationViewModel = locationViewModel;
            _nodeNavigationService = nodeNavigationService;
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
    }
}
