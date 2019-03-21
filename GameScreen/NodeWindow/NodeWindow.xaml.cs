using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameScreen.NodeWindow
{
    /// <summary>
    /// Interaction logic for NodeWindow.xaml
    /// </summary>
    public partial class NodeWindow : Window
    {
        private NodeWindowViewModel ViewModel => DataContext as NodeWindowViewModel;
        public NodeWindow(NodeWindowViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            this.Initialized += ViewModel.OnInitialized;
        }
    }
}
