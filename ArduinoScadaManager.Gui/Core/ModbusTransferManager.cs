using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using PrimS.Telnet;

namespace ArduinoScadaManager.Gui.Core
{
    public class ModbusTransferManager : IModbusTransferManager
    {
        private const int ReadCheckIntervalMilliseconds = 100;
        private const string ArduinoIp = "192.168.1.2";
        private const int SlavesPort = 28;
        private static readonly int[] AvailablePortsForMasters = {29,30,31,32};

        private readonly ILogger _logger;
        private bool _connected;

        private readonly Dictionary<byte, Client> _modbusMasterClients = new Dictionary<byte, Client>();
        private readonly Dictionary<byte, CancellationTokenSource> _modbusMastersClientsCancels = new Dictionary<byte, CancellationTokenSource>();
        private readonly Dictionary<byte, int> _modbusMasterClientPortAssociations = new Dictionary<byte, int>();
        
        private Client _modbusSlavesClient;
        private readonly CancellationTokenSource _modbusSlavesClientCancel = new CancellationTokenSource();

        public event Action<ModbusTransferData, byte> MastersDataReceived;
        public event Action<ModbusTransferData> SlavesDataReceived;

        public ModbusTransferManager(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<bool> ConnectMaster(byte masterIdentifier)
        {
            var cancellationToken = new CancellationTokenSource();
            int masterPort;
            Client masterClient = null;

            try
            {
                masterPort = GetAvailableMasterPort();

                if (_connected)
                {
                    await Task.Run(() =>
                    {
                        masterClient = new Client(ArduinoIp, masterPort, cancellationToken.Token);
                    }, cancellationToken.Token);

                    _logger.WriteDebug(string.Format("CONNECTED TO MASTER WITH ID:{0}", masterIdentifier));

                    RunReadingFromMaster(masterIdentifier, masterClient, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.WriteDebug(string.Format("!ERROR! While connecting to master port. {0}", ex.Message));
                return false;
            }

            _modbusMasterClientPortAssociations.Add(masterIdentifier, masterPort);

            if (_connected)
            {
                _modbusMastersClientsCancels.Add(masterIdentifier, cancellationToken);
                _modbusMasterClients.Add(masterIdentifier, masterClient);
            }

            return true;
        }

        private int GetAvailableMasterPort()
        {
            if (_modbusMasterClientPortAssociations.Count >= AvailablePortsForMasters.Length)
                throw new ArgumentException(string.Format("All master ports ({0}) are reserved actually.", AvailablePortsForMasters.Length));

            foreach (var port in AvailablePortsForMasters)
            {
                if(_modbusMasterClientPortAssociations.Values.Contains(port))
                    continue;

                return port;
            }
            throw new Exception("Error in port-master association collection.");
        }

        public void SendAsMaster(ModbusTransferData transferData, byte masterIdentifier)
        {
            _logger.WriteDebug("-------------------------------------------------------------------");

            if(!_connected)
                OnSlavesDataReceived(transferData.EncodeTransferData());
            else
                _modbusMasterClients[masterIdentifier].WriteLine(transferData.EncodeTransferData());
        }

        public void SendAsSlave(ModbusTransferData transferData)
        {
            if(!_connected)
                OnMastersDataReceived(transferData.EncodeTransferData(), 1);
            else
                _modbusSlavesClient.WriteLine(transferData.EncodeTransferData());
        }

        public async Task InitializeModbusSlaveTransfers()
        {
            if (_connected) return;

            try
            {
                await Task.Run(() =>
                {
                    _modbusSlavesClient = new Client(ArduinoIp, SlavesPort, _modbusSlavesClientCancel.Token);
                }, _modbusSlavesClientCancel.Token);

                _connected = true;
                _logger.WriteDebug("CONNECTED TO SLAVES PORT");

                RunReadingFromSlaves();
            }
            catch (Exception ex)
            {
                _logger.WriteDebug(string.Format("!ERROR! While connecting to slaves port. {0}", ex.Message));
            }
        }

        private void RunReadingFromMaster(byte masterIdentifier, Client masterClient, CancellationTokenSource cancellationToken)
        {
            Task.Run(async () =>
            {
                var builder = new StringBuilder();

                while (true)
                {
                    builder.Append(await masterClient.ReadAsync());
                    var index = builder.ToString().IndexOf("\r\n", StringComparison.Ordinal);

                    if (cancellationToken.Token.IsCancellationRequested)
                        return;

                    Thread.Sleep(ReadCheckIntervalMilliseconds);

                    if (index == -1)
                        continue;

                    OnMastersDataReceived(builder.ToString(0, index + 2), masterIdentifier);
                    builder.Remove(0, index + 2);
                }
            }, cancellationToken.Token);
        }

        private void RunReadingFromSlaves()
        {
            Task.Run(async () =>
            {
                var builder = new StringBuilder();

                while (true)
                {
                    builder.Append(await _modbusSlavesClient.ReadAsync());
                    var index = builder.ToString().IndexOf("\r\n", StringComparison.Ordinal);

                    if (_modbusSlavesClientCancel.Token.IsCancellationRequested)
                        return;

                    Thread.Sleep(ReadCheckIntervalMilliseconds);

                    if (index == -1)
                        continue;

                    OnSlavesDataReceived(builder.ToString(0, index + 2));
                    builder.Remove(0, index + 2);
                }
            });
        }

        private void OnMastersDataReceived(string data, byte masterIdentifier)
        {
            _logger.WriteDebug(String.Format("Masters data received: {0}", data.Trim()));

            ModbusTransferData decodedMessage;
            try
            {
                decodedMessage = new ModbusTransferData(data);
            }
            catch (ArgumentException)
            {
                _logger.WriteDebug("!ERROR! Problems during decoding.");
                return;
            }

            _logger.WriteDebug(String.Format("Decoded as: {0}", decodedMessage));

            if (MastersDataReceived != null)
                MastersDataReceived(decodedMessage, masterIdentifier);
        }

        private void OnSlavesDataReceived(string data)
        {
            _logger.WriteDebug(String.Format("Slaves data received: {0}", data.Trim()));

            ModbusTransferData decodedMessage;
            try
            {
                decodedMessage = new ModbusTransferData(data);
            }
            catch (ArgumentException)
            {
                _logger.WriteDebug("!ERROR! Problems during decoding.");
                return;
            }

            _logger.WriteDebug(String.Format("Decoded as: {0}", decodedMessage));

            if (SlavesDataReceived != null)
                SlavesDataReceived(decodedMessage);
        }

        public void Dispose()
        {
            foreach (var modbusMastersClientsCancel in _modbusMastersClientsCancels)
                modbusMastersClientsCancel.Value.Cancel();

            _modbusSlavesClientCancel.Cancel();
        }

        public void WriteDebug(string content)
        {
            _logger.WriteDebug(content);
        }
    }
}