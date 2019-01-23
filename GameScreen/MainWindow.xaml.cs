using System.Windows;

namespace GameScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewmodel viewmodel)
        {
            DataContext = viewmodel;
            InitializeComponent();
        }
    }
}
