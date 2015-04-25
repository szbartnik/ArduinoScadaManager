using System;
using System.Threading;
using System.Threading.Tasks;
using ArduinoScadaManager.Common.Core;
using PrimS.Telnet;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public partial class MainWindowViewModel
    {
        private const string ArduinoIp = "192.168.1.2";
        private const int MastersPort = 27;
        private const int SlavesPort = 28;

        private Client _modbusMastersClient;
        private Client _modbusSlavesClient;

        private readonly CancellationTokenSource _modbusMastersClientCancel = new CancellationTokenSource();
        private readonly CancellationTokenSource _modbusSlavesClientCancel = new CancellationTokenSource();

        public event Action<ModbusTransferData> MastersDataReceived;
        public event Action<ModbusTransferData> SlavesDataReceived;

        private void InitializeModbusTransfers()
        {
            Task.Run(() =>
            {
                _modbusMastersClient = new Client(ArduinoIp, MastersPort, _modbusMastersClientCancel.Token);
                _modbusSlavesClient = new Client(ArduinoIp, SlavesPort, _modbusSlavesClientCancel.Token);
            });
        }

        public void SendAsMaster(ModbusTransferData transferData)
        {
            _modbusMastersClient.WriteLine(transferData.EncodeTransferData());
        }

        public void SendAsSlave(ModbusTransferData transferData)
        {
            _modbusSlavesClient.WriteLine(transferData.EncodeTransferData());
        }

        private void DisposeModbusTransfer()
        {
            _modbusMastersClientCancel.Cancel();
            _modbusSlavesClientCancel.Cancel();
        }
    }
}
