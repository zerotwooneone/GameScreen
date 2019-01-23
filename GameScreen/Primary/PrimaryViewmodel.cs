using System.ComponentModel;
using System.Runtime.CompilerServices;
using GameScreen.Annotations;

namespace GameScreen.Primary
{
    public class PrimaryViewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
