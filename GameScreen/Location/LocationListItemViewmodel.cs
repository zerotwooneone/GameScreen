using System;
using System.Windows.Input;
using GameScreen.Viewmodel;
using Microsoft.Expression.Interactivity.Core;

namespace GameScreen.Location
{
    public class LocationListItemViewmodel : ViewModelBase
    {
        public delegate LocationListItemViewmodel Factory(string name,
            Action switchTo,
            Action openNew);

        public string Name { get; }
        public ICommand SwitchToCommand { get; }
        public ICommand OpenNewCommand { get; }

        public LocationListItemViewmodel(string name,
            Action switchTo,
            Action openNew)
        {
            Name = name;
            SwitchToCommand = new ActionCommand(switchTo);
            OpenNewCommand = new ActionCommand(openNew);
        }
    }
}
