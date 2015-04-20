using System.Windows.Controls;
using ArduinoScadaManager.Common.ViewModels.ScadaModuleProcessViewModel;

namespace ArduinoScadaManager.Gui.Views
{
    public partial class ScadaPanelView : UserControl
    {
        public ScadaPanelView(ScadaModuleProcess process)
        {
            InitializeComponent();
            DataContext = process;
        }
    }
}
