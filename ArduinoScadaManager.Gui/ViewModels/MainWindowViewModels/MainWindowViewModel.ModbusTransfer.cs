using System;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Gui.Core;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public partial class MainWindowViewModel
    {
        private readonly ModbusTransferManager _modbusTransferManager;

        public event Action<ModbusTransferData> MastersDataReceived;
        public event Action<ModbusTransferData> SlavesDataReceived;

        public void SendAsMaster(ModbusTransferData transferData)
        {
            _modbusTransferManager.SendAsMaster(transferData);
        }

        public void SendAsSlave(ModbusTransferData transferData)
        {
            _modbusTransferManager.SendAsSlave(transferData);
        }
    }
}
