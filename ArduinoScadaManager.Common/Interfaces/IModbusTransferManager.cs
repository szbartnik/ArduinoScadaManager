using System;
using System.Threading.Tasks;
using ArduinoScadaManager.Common.Core;

namespace ArduinoScadaManager.Common.Interfaces
{
    public interface IModbusTransferManager : IDisposable, ILogger
    {
        Task<bool> ConnectMaster(byte masterIdentifier);

        event Action<ModbusTransferData, byte> MastersDataReceived;
        event Action<ModbusTransferData> SlavesDataReceived;
        void SendAsMaster(ModbusTransferData transferData, byte masterIdentifier);
        void SendAsSlave(ModbusTransferData transferData);

        Task InitializeModbusSlaveTransfers();
    }
}