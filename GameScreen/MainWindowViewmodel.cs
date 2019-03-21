using GameScreen.Annotations;
using GameScreen.Primary;
using GameScreen.WpfCommand;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GameScreen.Location;
using GameScreen.Navigation;
using GameScreen.NodeWindow;

namespace GameScreen
{
    public class MainWindowViewmodel : INotifyPropertyChanged, INavigationContext
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
                var locationViewModel = _locationViewmodelFactory.Invoke(locationModel);
                var windowViewModel = _nodeWindowLocationFactory.Invoke(locationViewModel);
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
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Task GoToLocation(string locationId)
        {
            throw new NotImplementedException();
        }
    }
}
