using GameScreen.Viewmodel;

namespace GameScreen.Location
{
    public class NewRelatedLocationViewmodel: ViewModelBase
    {
        private string _name;
        private string _id;

        public delegate NewRelatedLocationViewmodel Factory(string id, string name);

        public NewRelatedLocationViewmodel(string id, string name)
        {
            _id = id;
            _name = name;
        }

        public string Id
        {
            get => _id;
            private set => SetProperty(ref _id, value);
        }
        public string Name
        {
            get => _name;
            private set => SetProperty(ref _name, value);
        }
    }
}