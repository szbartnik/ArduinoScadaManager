using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;

namespace ArduinoScadaManager.Common.ViewModels
{
    public abstract class SlaveModuleDevicePanelViewModelBase : ViewModelBase
    {
        private readonly IModbusTransferManager _modbusTransferManager;

        protected SlaveModuleDevicePanelViewModelBase(SlaveModuleProcessBase slaveModuleProcess)
        {
            _modbusTransferManager = slaveModuleProcess.Manager.ModbusTransferManager;
        }
    }
}
