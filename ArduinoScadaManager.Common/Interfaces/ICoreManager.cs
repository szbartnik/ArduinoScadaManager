using System;
using System.Collections.ObjectModel;
using ArduinoScadaManager.Common.Core;

namespace ArduinoScadaManager.Common.Interfaces
{
    public interface ICoreManager
    {
        event Action<SlaveModuleProcessBase> SlaveModuleAdded;
        event Action<SlaveModuleProcessBase> SlaveModuleRemoved;

        IModbusTransferManager ModbusTransferManager { get; }

        ObservableCollection<IMasterModuleProcess> ActiveMasterScadaDevices { get; }
        ObservableCollection<SlaveModuleProcessBase> ActiveSlaveDevices { get; }
        void RemoveSlaveModule(SlaveModuleProcessBase slaveModuleProcessBase);
        void RemoveScadaModule(IMasterModuleProcess masterModuleProcess);
        byte GenerateSlaveModuleIdentifier();
        byte GenerateMasterModuleIdentifier();
    }
}