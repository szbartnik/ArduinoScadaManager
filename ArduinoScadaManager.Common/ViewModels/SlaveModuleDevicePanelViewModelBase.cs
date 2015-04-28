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

            PrepareUsageOfModbusTransferManager();
        }

        private void PrepareUsageOfModbusTransferManager()
        {
            _modbusTransferManager.SlavesDataReceived += OnSlavesDataReceived;
        }

        private void OnSlavesDataReceived(ModbusTransferData modbusTransferData)
        {
            if (modbusTransferData.DeviceAddress == 0)
                OnDataReceived(modbusTransferData);

            if(modbusTransferData.DeviceAddress == _slaveModuleProcess.Identifier)
                OnDataReceived(modbusTransferData);
        }

        protected void SendResponse(byte command, string data = "")
        {
            SendResponse(command, data.StringToByteArray());
        }

        protected void SendResponse(byte command, byte[] data)
        {
            if(data == null) data = new byte[0];

            _modbusTransferManager.SendAsSlave(new ModbusTransferData(
                _slaveModuleProcess.Identifier, command, data));
        }

        protected abstract void OnDataReceived(ModbusTransferData modbusTransferData);
    }
}
