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
        private Lazy<PrimaryWindow> _primaryWindow;

        public MainWindowViewmodel(Func<PrimaryWindow> primaryWindowFactory)
        {
            _primaryWindowFactory = primaryWindowFactory;
            _primaryWindow = new Lazy<PrimaryWindow>(_primaryWindowFactory);
            TestCommand = new RelayCommand(
                obj => !_primaryWindow.IsValueCreated, 
                obj =>
            {
                _primaryWindow.Value.Show();
                _primaryWindow.Value.Closed += HandlePrimaryClosed;
            });
        }

        private void HandlePrimaryClosed(object sender, EventArgs e)
        {
            _primaryWindow = new Lazy<PrimaryWindow>(_primaryWindowFactory);
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
