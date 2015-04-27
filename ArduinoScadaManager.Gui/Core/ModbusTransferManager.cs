using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Interfaces;
using PrimS.Telnet;

namespace ArduinoScadaManager.Gui.Core
{
    public class ModbusTransferManager : IModbusTransferManager
    {
        private const string ArduinoIp = "192.168.1.2";
        private const int MastersPort = 27;
        private const int SlavesPort = 28;

        private readonly ILogger _logger;

        private Client _modbusMastersClient;
        private Client _modbusSlavesClient;

        private readonly CancellationTokenSource _modbusMastersClientCancel = new CancellationTokenSource();
        private readonly CancellationTokenSource _modbusSlavesClientCancel = new CancellationTokenSource();

        public event Action<ModbusTransferData> MastersDataReceived;
        public event Action<ModbusTransferData> SlavesDataReceived;

        public ModbusTransferManager(ILogger logger)
        {
            _logger = logger;
            InitializeModbusTransfers();
        }

        public void SendAsMaster(ModbusTransferData transferData)
        {
            _modbusMastersClient.WriteLine(transferData.EncodeTransferData());
        }

        public void SendAsSlave(ModbusTransferData transferData)
        {
            _modbusSlavesClient.WriteLine(transferData.EncodeTransferData());
        }

        private void InitializeModbusTransfers()
        {
            _modbusMastersClient = new Client(ArduinoIp, MastersPort, _modbusMastersClientCancel.Token);
            _modbusSlavesClient = new Client(ArduinoIp, SlavesPort, _modbusSlavesClientCancel.Token);

            RunReadingFromMasters();
            RunReadingFromSlaves();
        }

        private void RunReadingFromMasters()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    var builder = new StringBuilder();
                    while (true)
                    {
                        builder.Append(await _modbusMastersClient.ReadAsync());
                        var index = builder.ToString().IndexOf("\r\n", StringComparison.Ordinal);
                        if (index == -1)
                            continue;

                        OnMastersDataReceived(builder.ToString(0, index + 2));
                        builder.Remove(0, index + 2);

                        if (_modbusMastersClientCancel.Token.IsCancellationRequested)
                            return;
                    }
                }
            });
        }

        private void RunReadingFromSlaves()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    var builder = new StringBuilder();
                    while (true)
                    {
                        builder.Append(await _modbusSlavesClient.ReadAsync());
                        var index = builder.ToString().IndexOf("\r\n", StringComparison.Ordinal);
                        if (index == -1)
                            continue;

                        OnSlavesDataReceived(builder.ToString(0, index + 2));
                        builder.Remove(0, index + 2);

                        if (_modbusSlavesClientCancel.Token.IsCancellationRequested)
                            return;
                    }
                }
            });
        }

        private void OnMastersDataReceived(string data)
        {
            var decodedMessage = new ModbusTransferData(data);
            _logger.WriteDebug(String.Format("Masters data received: {0}. Decoded as {1}", data.Trim(), decodedMessage));

            if(MastersDataReceived != null)
                MastersDataReceived(decodedMessage);
        }

        private void OnSlavesDataReceived(string data)
        {
            var decodedMessage = new ModbusTransferData(data);
            _logger.WriteDebug(String.Format("Slaves data received: {0}. Decoded as {1}", data.Trim(), decodedMessage));

            if (SlavesDataReceived != null)
                SlavesDataReceived(decodedMessage);
        }

        public void Dispose()
        {
            _modbusMastersClientCancel.Cancel();
            _modbusSlavesClientCancel.Cancel();
        }
    }
}