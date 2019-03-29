using System;
using System.Windows.Input;
using GameScreen.Viewmodel;
using Microsoft.Expression.Interactivity.Core;

namespace GameScreen.BreadCrumb
{
    public class BreadCrumbViewmodel : ViewModelBase
    {
        public delegate BreadCrumbViewmodel Factory(string displayText, Action onClicked);

        public string DisplayText { get; }
        public ICommand ClickCommand { get; }

        public BreadCrumbViewmodel(string displayText, Action onClicked)
        {
            DisplayText = displayText;
            ClickCommand = new ActionCommand(onClicked);
        }
    }
}
