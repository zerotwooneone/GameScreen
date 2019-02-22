using GameScreen.Annotations;
using GameScreen.MobStat;
using GameScreen.Primary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameScreen.StatBlock
{
    public class StatBlockViewmodel : INotifyPropertyChanged, IPositionable, ISizable
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Guid Id { get; }
        public string Name { get; set; }
        public ObservableCollection<MobStatViewmodel> Stats { get; }

        public StatBlockViewmodel(Guid id, string name, IEnumerable<MobStatViewmodel> mobStats, double x, double y, double height, double width)
        {
            Id = id;
            Name = name;
            X = x;
            Y = y;
            Height = height;
            Width = width;
            Stats = new ObservableCollection<MobStatViewmodel>(mobStats);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}