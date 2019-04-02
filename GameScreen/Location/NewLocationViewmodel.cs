using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GameScreen.MongoDb;
using GameScreen.Viewmodel;
using GameScreen.WpfCommand;
using MongoDB.Bson;

namespace GameScreen.Location
{
    public class NewLocationViewmodel : ViewModelBase
    {
        private readonly Action _closeCallback;
        private readonly IGameScreenContext _gameScreenContext;
        private string _name;

        public delegate NewLocationViewmodel Factory(IEnumerable<NewRelatedLocationViewmodel> relatedLocations,
            Action closeCallback);

        public NewLocationViewmodel(IEnumerable<NewRelatedLocationViewmodel> relatedLocations,
            Action closeCallback,
            IGameScreenContext gameScreenContext)
        {
            _closeCallback = closeCallback;
            _gameScreenContext = gameScreenContext;
            SaveCommand = new AwaitableDelegateCommand(OnSaveClicked);
            Locations = new ObservableCollection<NewRelatedLocationViewmodel>(relatedLocations);
        }

        private async Task OnSaveClicked()
        {
            var locationModel= new LocationModel
            {
                DmMapUrl = DmMapUrl,
                Name = Name,
                PlayerMapUrl = PlayerMapUrl,
                RelatedLocation = Locations.Select(vm=>ObjectId.Parse(vm.Id)).ToList()
            };
            await _gameScreenContext.AddLocations(new[] {locationModel});
            _closeCallback();
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ObservableCollection<NewRelatedLocationViewmodel> Locations { get; }

        public ICommand SaveCommand { get; }

        public string DmMapUrl { get; set; }
        public string PlayerMapUrl { get; set; }
    }
}
