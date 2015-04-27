using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;

namespace ArduinoScadaManager.Common.ViewModels
{
    public abstract class SlaveModuleDevicePanelViewModelBase : ViewModelBase
    {
        private readonly IModbusTransferManager _modbusTransferManager;
        private readonly SlaveModuleProcessBase _slaveModuleProcess;

        protected SlaveModuleDevicePanelViewModelBase(IModbusTransferManager modbusTransferManager, SlaveModuleProcessBase slaveModuleProcess)
        {
            _modbusTransferManager = modbusTransferManager;
            _slaveModuleProcess = slaveModuleProcess;
        }
    }
}
