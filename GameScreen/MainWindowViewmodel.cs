using GameScreen.Annotations;
using GameScreen.Primary;
using GameScreen.WpfCommand;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GameScreen.Location;
using GameScreen.NodeWindow;
using MongoDB.Driver;

namespace GameScreen
{
    public class MainWindowViewmodel : INotifyPropertyChanged
    {
        private readonly Func<PrimaryWindow> _primaryWindowFactory;
        private readonly NodeWindowViewModel.LocationFactory _nodeWindowLocationFactory;
        private readonly LocationViewmodel.Factory _locationViewmodelFactory;
        private Lazy<PrimaryWindow> _primaryWindow;

        public MainWindowViewmodel(Func<PrimaryWindow> primaryWindowFactory,
            NodeWindowViewModel.LocationFactory nodeWindowLocationFactory,
            LocationViewmodel.Factory locationViewmodelFactory)
        {
            _primaryWindowFactory = primaryWindowFactory;
            _nodeWindowLocationFactory = nodeWindowLocationFactory;
            _locationViewmodelFactory = locationViewmodelFactory;
            _primaryWindow = new Lazy<PrimaryWindow>(_primaryWindowFactory);
            TestCommand = new RelayCommand(
                obj => !_primaryWindow.IsValueCreated,
                obj =>
            {
                _primaryWindow.Value.Show();
                _primaryWindow.Value.Closed += HandlePrimaryClosed;
            });
            MongoCommand = new RelayCommand(param=>true, param =>
            {
                var client = new MongoClient(new MongoClientSettings
                {
                    Server = new MongoServerAddress("localhost", 27017),
                    
                });
                var gameScreenDb = client.GetDatabase("gameScreenDb");
                var locations = gameScreenDb.GetCollection<LocationModel>("locations");
                var locs = locations.Find(FilterDefinition<LocationModel>.Empty).ToList();

                var locationModel = locs.First();
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
    }
}
