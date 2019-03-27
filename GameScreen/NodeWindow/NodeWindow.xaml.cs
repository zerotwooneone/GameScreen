using System.Windows;

namespace GameScreen.NodeWindow
{
    /// <summary>
    /// Interaction logic for NodeWindow.xaml
    /// </summary>
    public partial class NodeWindow : Window
    {
        public delegate NodeWindow Factory(NodeWindowViewModel viewModel);
        
        /// <summary>
        /// This only exists to support unit testing
        /// </summary>
        protected NodeWindow(){}

        public NodeWindow(NodeWindowViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        /// <summary>
        /// This only exists to support unit testing
        /// </summary>
        public new virtual void Show()
        {
            base.Show();
        }
    }
}
