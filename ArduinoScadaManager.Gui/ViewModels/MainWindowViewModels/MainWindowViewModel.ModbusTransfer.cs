using System;
using ArduinoScadaManager.Common.Core;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public partial class MainWindowViewModel
    {
        public event Action<ModbusTransferData> MastersDataReceived;
        public event Action<ModbusTransferData> SlavesDataReceived;
        public void SendAsMaster(ModbusTransferData transferData)
        {
            throw new NotImplementedException();
        }

        public void SendAsSlave(ModbusTransferData transferData)
        {
            throw new NotImplementedException();
        }
    }
}
