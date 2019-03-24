using System;
using System.Windows.Input;

namespace GameScreen.NodeWindow
{
    public abstract class ViewModelBase : BindableBase
    {
        public virtual ICommand LoadedCommand => null;
    }
}