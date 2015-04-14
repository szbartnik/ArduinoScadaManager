using System.Windows;
using ArduinoScadaManager.Gui.ViewModels;
using ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels;

namespace ArduinoScadaManager.Gui.Views
{
    public partial class MainWindowView : Window
    {
        public MainWindowView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
