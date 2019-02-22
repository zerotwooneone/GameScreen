using GameScreen.Annotations;
using GameScreen.MobStat;
using GameScreen.Persistence;
using GameScreen.StatBlock;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GameScreen.Primary
{
    public class PrimaryViewmodel : INotifyPropertyChanged
    {
        public PrimaryViewmodel(IPersistenceService persistenceService)
        {
            PrimaryDatamodel datamodel = persistenceService.Get();
            StatBlocks = ConvertToStatBlocks(datamodel.Mobs);
        }

        private ObservableCollection<StatBlockViewmodel> ConvertToStatBlocks(IEnumerable<MobDatamodel> datamodelMobs)
        {
            List<StatBlockViewmodel> statViewModels = datamodelMobs.Select(ConvertToViewModel).ToList();
            ObservableCollection<StatBlockViewmodel> blocks = new ObservableCollection<StatBlockViewmodel>(statViewModels);
            return blocks;
        }

        private StatBlockViewmodel ConvertToViewModel(MobDatamodel mobDatamodel)
        {
            IEnumerable<MobStatViewmodel> statViewModels = mobDatamodel.MobStats.Select(ConvertToStat);
            return new StatBlockViewmodel(mobDatamodel.Id, mobDatamodel.Name, statViewModels, mobDatamodel.X, mobDatamodel.Y, mobDatamodel.Height, mobDatamodel.Width);
        }

        private MobStatViewmodel ConvertToStat(MobStatDatamodel datamodel)
        {
            return new MobStatViewmodel(datamodel.Name, datamodel.Pinned, datamodel.Value);
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
