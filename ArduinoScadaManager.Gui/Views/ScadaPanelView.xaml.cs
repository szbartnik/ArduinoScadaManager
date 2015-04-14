using System.Windows.Controls;
using ArduinoScadaManager.Gui.ViewModels;
using ArduinoScadaManager.Gui.ViewModels.ScadaPanelViewModels;

namespace ArduinoScadaManager.Gui.Views
{
    public partial class ScadaPanelView : UserControl
    {
        public ScadaPanelView(ScadaPanelViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
