using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GameScreen.Annotations;
using GameScreen.Primary;

namespace GameScreen
{
    public class MainWindowViewmodel : INotifyPropertyChanged
    {
        private readonly Func<PrimaryWindow> _primaryWindowFactory;

        public MainWindowViewmodel(Func<PrimaryWindow> primaryWindowFactory)
        {
            _primaryWindowFactory = primaryWindowFactory;
            TestCommand = new RelayCommand(obj=>true, obj =>
            {
                var primary = _primaryWindowFactory();
                primary.Show();
            });
        }
        public ICommand TestCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
