using System.Windows.Controls;
using ArduinoScadaManager.Gui.ViewModels.ScadaModuleProcessViewModels;

namespace ArduinoScadaManager.Gui.Views
{
    public partial class ScadaPanelView : UserControl
    {
        public ScadaPanelView(ScadaModuleProcessViewModel jakasKlasa)
        {
            InitializeComponent();
            DataContext = jakasKlasa;
        }
    }
}
