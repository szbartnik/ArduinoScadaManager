using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using ArduinoScadaManager.Common.Core;

namespace ArduinoScadaManager.Common.Interfaces
{
    public interface ICoreManager
    {
        event Action<SlaveModuleProcessBase> SlaveModuleAdded;
        event Action<SlaveModuleProcessBase> SlaveModuleRemoved;

        event Action<SlaveModuleProcessBase> MasterModuleAdded;
        event Action<SlaveModuleProcessBase> MasterModuleRemoved;

        void SendAsMaster(ModbusTransferData transferData);
        void SendAsSlave(ModbusTransferData transferData);

        ObservableCollection<IMasterModuleProcess> ActiveMasterScadaDevices { get; }
        ObservableCollection<SlaveModuleProcessBase> ActiveSlaveDevices { get; }
        void RemoveSlaveModule(SlaveModuleProcessBase slaveModuleProcessBase);
        void RemoveScadaModule(IMasterModuleProcess jakasKlasa);
        int GenerateSlaveModuleIdentifier();
        int GenerateMasterModuleIdentifier();
    }
}