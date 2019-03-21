namespace GameScreen.Navigation
{
    public class NavigationParam : INavigationParam
    {
        public NavigationParam(string locationId, bool newWindow)
        {
            LocationId = locationId;
            NewWindow = newWindow;
        }

        public string LocationId { get; }
        public bool NewWindow { get; }
    }
}