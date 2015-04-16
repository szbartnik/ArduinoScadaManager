using System.ComponentModel.Composition;
using ArduinoScadaManager.Common.Core;

namespace WaterPumpModule.Core
{
    [Export(typeof(ISlaveModule))]
    public class SlaveModule : ISlaveModule
    {
        public IInterface1 GetInterfaceImplementation(ICoreManager manager)
        {
            return new WaterPumpInterface1Implementation(manager);
        }
    }

    public class WaterPumpInterface1Implementation : IInterface1
    {
        private readonly ICoreManager _manager;

        public WaterPumpInterface1Implementation(ICoreManager manager)
        {
            _manager = manager;
        }

        public SlaveModuleDevicePanelViewModelBase DevicePanelViewModel { get; set; }
        public SlaveModuleScadaPanelViewModelBase ScadaPanelViewModel { get; set; }
    }
}
