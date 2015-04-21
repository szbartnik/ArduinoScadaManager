using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Interfaces;
using WaterPumpModule.ViewModels;
using WaterPumpModule.Views;

namespace WaterPumpModule.Core
{
    public class WaterPumpSlaveModuleProcess : SlaveModuleProcessBase
    {
        private readonly ICoreManager _manager;

        public override sealed UserControl SlaveModuleDevicePanelView { get; set; }

        public WaterPumpSlaveModuleProcess(ICoreManager manager)
            : base(manager)
        {
            _manager = manager;

            SlaveModuleDevicePanelView = new WaterPumpModuleDevicePanelView(
                new WaterPumpModuleDevicePanelViewModel(_manager));
        }

        public override UserControl GetScadaPanelViewOfSlaveModule(IMasterModuleProcess scadaModuleProcess)
        {
            return new WaterPumpModuleScadaPanelView(
                new WaterPumpModuleScadaPanelViewModel(scadaModuleProcess));
        }
    }
}