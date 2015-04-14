using System.Windows.Controls;
using ArduinoScadaManager.Gui.ViewModels;

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
