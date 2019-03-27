﻿using GameScreen.Primary;
using GameScreen.WpfCommand;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GameScreen.Location;
using GameScreen.Node;
using GameScreen.NodeHistory;
using GameScreen.NodeWindow;
using GameScreen.Viewmodel;
using Microsoft.Expression.Interactivity.Core;

namespace GameScreen
{
    public class MainWindowViewmodel : ViewModelBase
    {
        private readonly Func<PrimaryWindow> _primaryWindowFactory;
        private readonly NodeWindowViewModel.LocationFactory _nodeWindowLocationFactory;
        private readonly LocationViewmodel.Factory _locationViewmodelFactory;
        private readonly ILocationService _locationService;
        private readonly INodeNavigationService _nodeNavigationService;
        private readonly IWindowService _windowService;
        private Lazy<PrimaryWindow> _primaryWindow;
        public ICommand LoadedCommand { get; }

        public MainWindowViewmodel(Func<PrimaryWindow> primaryWindowFactory,
            NodeWindowViewModel.LocationFactory nodeWindowLocationFactory,
            LocationViewmodel.Factory locationViewmodelFactory,
            ILocationService locationService,
            INodeNavigationService nodeNavigationService,
            IWindowService windowService)
        {
            _primaryWindowFactory = primaryWindowFactory;
            _nodeWindowLocationFactory = nodeWindowLocationFactory;
            _locationViewmodelFactory = locationViewmodelFactory;
            _locationService = locationService;
            _nodeNavigationService = nodeNavigationService;
            _windowService = windowService;
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
                var locationId = "5c9174b4a1effb00d8cba037";
                await OpenNewLocationWindow(locationId);
            });
            LoadedCommand = new ActionCommand(OnLoaded);
        }

        private async Task OpenNewLocationWindow(string locationId)
        {
            var locationModel = await _locationService.GetLocationById(locationId);
            var current = new HistoryNode(locationModel.Name, locationModel.Id.ToString());
            var nodeHistoryState = new NodeHistoryState(current, 100);
            var windowViewModel = _nodeWindowLocationFactory.Invoke(locationModel, nodeHistoryState);
            await _windowService.OpenNewNode(windowViewModel);
        }

        private void OnLoaded()
        {
            var navReplayable = _nodeNavigationService
                .NavigationObservable
                .SelectMany(async navigationParam =>
                {
                    await OpenNewLocationWindow(navigationParam.LocationId);
                    return (string) null;
                })
                .Replay();
            navReplayable.Connect();
        }

        private void HandlePrimaryClosed(object sender, EventArgs e)
        {
            _primaryWindow = new Lazy<PrimaryWindow>(_primaryWindowFactory);
        }

        public ICommand TestCommand { get; }
        public ICommand MongoCommand { get; }
        
    }
}
