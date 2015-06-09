using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.ViewModels;

namespace WaterPumpModule.ViewModels
{
    public class WaterPumpModuleDevicePanelViewModel : SlaveModuleDevicePanelViewModelBase
    {
        public WaterPumpModuleDevicePanelViewModel(IModbusTransferManager modbusTransferManager, SlaveModuleProcessBase slaveModuleProcess) 
            : base(modbusTransferManager, slaveModuleProcess)
        {

        }
    }
}
