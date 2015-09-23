using System.Windows.Controls;
using ArduinoScadaManager.Common.Infrastructure._2dControl;
using WaterPumpModule.ViewModels;

namespace WaterPumpModule.Views
{
    public partial class WaterPumpModuleDevicePanelView : UserControl
    {
        public WaterPumpModuleDevicePanelView(WaterPumpModuleDevicePanelViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            //ItemsControl.ItemsSource = BindableListHelper.GetBindable2DList(viewModel.Checkboxes);
        }
    }
}
