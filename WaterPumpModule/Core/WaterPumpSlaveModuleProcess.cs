using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using WaterPumpModule.ViewModels;
using WaterPumpModule.Views;

namespace WaterPumpModule.Core
{
    public class WaterPumpSlaveModuleProcess : SlaveModuleProcessBase
    {
        private readonly ICoreManager _manager;

        public WaterPumpSlaveModuleProcess(ICoreManager manager)
            : base(manager)
        {
            _manager = manager;
            SlaveModuleDevicePanelView = new WaterPumpModuleDevicePanelView(
                new WaterPumpModuleDevicePanelViewModel(_manager));
        }

        public override UserControl SlaveModuleDevicePanelView { get; set; }
    }
}