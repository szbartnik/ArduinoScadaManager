using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.Models;

namespace ArduinoScadaManager.Common.ViewModels
{
    public abstract class SlaveModuleDevicePanelViewModelBase : ViewModelBase
    {
        private const int ErrorCommand = 255;

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

        protected void SendErrorResponse(string errorMessage)
        {
            // TODO
            //SendResponse(ErrorCommand, errorMessage);
        }

        private void SendResponse(ModbusCommand command, byte[] data)
        {
            if(data == null) data = new byte[0];

            _modbusTransferManager.SendAsSlave(new ModbusTransferData(
                _slaveModuleProcess.Identifier, command, data));
        }

        protected abstract void OnDataReceived(ModbusTransferData modbusTransferData);
    }
}
