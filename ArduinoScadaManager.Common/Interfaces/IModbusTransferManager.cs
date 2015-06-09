using System;
using System.Threading.Tasks;
using ArduinoScadaManager.Common.Core;

namespace ArduinoScadaManager.Common.Interfaces
{
    public interface IModbusTransferManager : IDisposable, ILogger
    {
        event Action<ModbusTransferData> MastersDataReceived;
        event Action<ModbusTransferData> SlavesDataReceived;
        void SendAsMaster(ModbusTransferData transferData);
        void SendAsSlave(ModbusTransferData transferData);

        Task InitializeModbusTransfers();
    }
}