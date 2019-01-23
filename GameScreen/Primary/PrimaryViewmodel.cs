using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GameScreen.Annotations;

namespace GameScreen.Primary
{
    public class PrimaryViewmodel : INotifyPropertyChanged
    {
        public PrimaryViewmodel()
        {
            StatBlocks = new ObservableCollection<RectItem>();
            StatBlocks.Add(new RectItem{Height = 100, Width = 100, X = 10, Y = 20, Name = "one", IsAlive = "Alive"});
            StatBlocks.Add(new RectItem{Height = 100, Width = 100, X = 150, Y = 120, Name = "two", IsAlive = "Dead"});
        }
        public ObservableCollection<RectItem> StatBlocks { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RectItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Name { get; set; }
        public string IsAlive { get; set; }
    }
}
