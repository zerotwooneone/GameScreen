using System.Windows;

namespace GameScreen.Primary
{
    /// <summary>
    /// Interaction logic for PrimaryWindow.xaml
    /// </summary>
    public partial class PrimaryWindow : Window
    {
        public PrimaryWindow(PrimaryViewmodel viewmodel)
        {
            DataContext = viewmodel;
            InitializeComponent();
        }
    }
}
