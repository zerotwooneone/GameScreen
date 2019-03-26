using GameScreen.Primary;
using GameScreen.WpfCommand;
using System;
using System.Windows.Input;
using GameScreen.Location;
using GameScreen.NodeHistory;
using GameScreen.NodeWindow;
using GameScreen.Viewmodel;

namespace GameScreen
{
    public class MainWindowViewmodel : ViewModelBase
    {
        private readonly Func<PrimaryWindow> _primaryWindowFactory;
        private readonly NodeWindowViewModel.LocationFactory _nodeWindowLocationFactory;
        private readonly LocationViewmodel.Factory _locationViewmodelFactory;
        private readonly ILocationService _locationService;
        private Lazy<PrimaryWindow> _primaryWindow;

        public MainWindowViewmodel(Func<PrimaryWindow> primaryWindowFactory,
            NodeWindowViewModel.LocationFactory nodeWindowLocationFactory,
            LocationViewmodel.Factory locationViewmodelFactory,
            ILocationService locationService)
        {
            _primaryWindowFactory = primaryWindowFactory;
            _nodeWindowLocationFactory = nodeWindowLocationFactory;
            _locationViewmodelFactory = locationViewmodelFactory;
            _locationService = locationService;
            _primaryWindow = new Lazy<PrimaryWindow>(_primaryWindowFactory);
            TestCommand = new RelayCommand(
                obj => !_primaryWindow.IsValueCreated,
                obj =>
            {
                _primaryWindow.Value.Show();
                _primaryWindow.Value.Closed += HandlePrimaryClosed;
            });
            MongoCommand = new RelayCommand(param=>true, async param =>
            {
                var locationModel = await _locationService.GetLocationById("5c9174b4a1effb00d8cba037");
                //var locationViewModel = _locationViewmodelFactory.Invoke(locationModel);
                var current = new HistoryNode(locationModel.Name, locationModel.Id.ToString());
                var nodeHistoryState = new NodeHistoryState(current, 100);
                var windowViewModel = _nodeWindowLocationFactory.Invoke(locationModel, nodeHistoryState);
                var nodeWindow = new NodeWindow.NodeWindow(windowViewModel);
                
                nodeWindow.Show();
            });
        }

        private void HandlePrimaryClosed(object sender, EventArgs e)
        {
            _primaryWindow = new Lazy<PrimaryWindow>(_primaryWindowFactory);
        }

        public ICommand TestCommand { get; }
        public ICommand MongoCommand { get; }
        
    }
}
