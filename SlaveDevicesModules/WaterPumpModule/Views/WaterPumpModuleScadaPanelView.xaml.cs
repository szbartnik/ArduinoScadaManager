using System.Windows.Controls;
using WaterPumpModule.ViewModels;

namespace WaterPumpModule.Views
{
    public partial class WaterPumpModuleScadaPanelView : UserControl
    {
        public WaterPumpModuleScadaPanelView(WaterPumpModuleScadaPanelViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
