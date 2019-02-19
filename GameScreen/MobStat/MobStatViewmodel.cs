using GameScreen.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameScreen.MobStat
{
    public class MobStatViewmodel : INotifyPropertyChanged
    {
        private string _name;
        private bool _pinned;
        private int _value;

        public MobStatViewmodel(string name, bool pinned, int value)
        {
            _pinned = pinned;
            Name = name;
            Value = value;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Value
        {
            get => _value;
            set
            {
                if (value == _value) return;
                _value = value;
                OnPropertyChanged();
            }
        }

        public bool Pinned
        {
            get => _pinned;
            set
            {
                if (value == _pinned) return;
                _pinned = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}