using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GameScreen.Annotations;
using GameScreen.MobStat;

namespace GameScreen.Primary
{
    public class PrimaryViewmodel : INotifyPropertyChanged
    {
        public PrimaryViewmodel()
        {
            var statDataModels = new[]
            {
                new MobStatDatamodel
                {
                    Name = "Stat 1",
                    Value = 1,
                    Pinned = false
                }
            };
            var mobStatViewmodels = statDataModels.Select(Convert);
            Stats = new ObservableCollection<MobStatViewmodel>();
            StatBlocks = new ObservableCollection<StatBlock.StatBlockViewmodel>();
            StatBlocks.Add(new StatBlock.StatBlockViewmodel(mobStatViewmodels)
            {
                Height = 100, Width = 100, X = 10, Y = 20, Name = "one"
            });
            StatBlocks.Add(new StatBlock.StatBlockViewmodel(new MobStatViewmodel[0])
                {Height = 100, Width = 100, X = 150, Y = 120, Name = "two"});
        }

        private MobStatViewmodel Convert(MobStatDatamodel arg)
        {
            var stat = ConvertToStat(arg);
            return stat;
        }

        private MobStatViewmodel ConvertToStat(MobStatDatamodel datamodel)
        {
            return new MobStatViewmodel(datamodel.Name, datamodel.Pinned);
        }

        public ObservableCollection<StatBlock.StatBlockViewmodel> StatBlocks { get; }
        public ObservableCollection<MobStatViewmodel> Stats { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
