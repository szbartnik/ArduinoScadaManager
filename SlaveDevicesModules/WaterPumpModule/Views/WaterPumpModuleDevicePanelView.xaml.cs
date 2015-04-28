using System.Windows.Controls;
using WaterPumpModule.ViewModels;

namespace WaterPumpModule.Views
{
    public partial class WaterPumpModuleDevicePanelView : UserControl
    {
        public WaterPumpModuleDevicePanelView(WaterPumpModuleDevicePanelViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
