using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;

namespace ArduinoScadaManager.Common.ViewModels
{
    public abstract class SlaveModuleScadaPanelViewModelBase : ViewModelBase
    {
        public SlaveModuleProcessBase SlaveModuleProcessBase { get; private set; }
        public IMasterModuleProcess MasterModuleProcess;

        protected IModbusTransferManager ModbusTransferManager { get; private set; }

        public UserControl View { get; set; }

        protected SlaveModuleScadaPanelViewModelBase(
            IModbusTransferManager modbusTransferManager,
            IMasterModuleProcess masterModuleProcess, 
            SlaveModuleProcessBase slaveModuleProcessBase)
        {
            ModbusTransferManager = modbusTransferManager;
            SlaveModuleProcessBase = slaveModuleProcessBase;
            MasterModuleProcess = masterModuleProcess;
        }
    }
}
