using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GameScreen.Annotations;
using GameScreen.MobStat;
using GameScreen.Primary;

namespace GameScreen.StatBlock
{
    public class StatBlockViewmodel : INotifyPropertyChanged, IPositionable, ISizable
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Name { get; set; }
        public ObservableCollection<MobStatViewmodel> Stats { get; }

        public StatBlockViewmodel(string name, IEnumerable<MobStatViewmodel> mobStats, double x, double y, double height, double width)
        {
            Name = name;
            X = x;
            Y = y;
            Height = height;
            Width = width;
            Stats = new ObservableCollection<MobStatViewmodel>(mobStats);
            Pinned = new ObservableCollection<MobStatViewmodel>(mobStats.Where(s=>s.Pinned));
        }

        public ObservableCollection<MobStatViewmodel> Pinned { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}