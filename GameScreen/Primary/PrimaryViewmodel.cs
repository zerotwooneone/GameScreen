using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GameScreen.Annotations;
using GameScreen.MobStat;
using GameScreen.Persistence;
using GameScreen.StatBlock;

namespace GameScreen.Primary
{
    public class PrimaryViewmodel : INotifyPropertyChanged
    {
        public PrimaryViewmodel(IPersistenceService persistenceService)
        {
            var datamodel = persistenceService.Get();
            StatBlocks = ConvertToStatBlocks(datamodel.Mobs);
        }

        private ObservableCollection<StatBlockViewmodel> ConvertToStatBlocks(IEnumerable<MobDatamodel> datamodelMobs)
        {
            var statViewModels = datamodelMobs.Select(ConvertToViewModel).ToList();
            var blocks = new ObservableCollection<StatBlockViewmodel>(statViewModels);
            return blocks;
        }

        private StatBlockViewmodel ConvertToViewModel(MobDatamodel mobDatamodel)
        {
            var statViewModels = mobDatamodel.MobStats.Select(Convert);
            return new StatBlockViewmodel(mobDatamodel.Name, statViewModels, mobDatamodel.X, mobDatamodel.Y, mobDatamodel.Height, mobDatamodel.Width);
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

        public ObservableCollection<StatBlockViewmodel> StatBlocks { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
