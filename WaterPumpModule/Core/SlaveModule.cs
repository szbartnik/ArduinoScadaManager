using System.ComponentModel.Composition;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using WaterPumpModule.ViewModels;
using WaterPumpModule.Views;

namespace WaterPumpModule.Core
{
    [Export(typeof(ISlaveModule))]
    public class SlaveModule : ISlaveModule
    {
        public UserControl GetModuleDevicePanelView()
        {
            return new WaterPumpModuleDevicePanelView(
                new WaterPumpModuleDevicePanelViewModel());
        }

        public UserControl GetModuleScadaPanelView()
        {
            return new WaterPumpModuleScadaPanelView(
                new WaterPumpModuleScadaPanelViewModel());
        }
    }
}
