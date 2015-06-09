using System;
using System.Collections;
using System.Linq;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.Models;

namespace ArduinoScadaManager.Common.ViewModels
{
    public abstract class SlaveModuleScadaPanelViewModelBase : ViewModelBase
    {
        protected const int ErrorCommand = 255;

        private readonly IModbusTransferManager _modbusTransferManager;
        protected readonly ILogger Logger;

        public ISlaveModuleProcess SlaveModuleProcess { get; private set; }
        public IMasterModuleProcess MasterModuleProcess { get; private set; }

        public UserControl View { get; set; }

        protected SlaveModuleScadaPanelViewModelBase(
            IModbusTransferManager modbusTransferManager,
            IMasterModuleProcess masterModuleProcess, 
            ISlaveModuleProcess slaveModuleProcess)
        {
            Logger = modbusTransferManager;
            _modbusTransferManager = modbusTransferManager;
            SlaveModuleProcess = slaveModuleProcess;
            MasterModuleProcess = masterModuleProcess;

            PrepareUsageOfModbusTransferManager();
        }

        private void PrepareUsageOfModbusTransferManager()
        {
            _modbusTransferManager.MastersDataReceived += OnMastersDataReceived;
        }

        private void OnMastersDataReceived(ModbusTransferData modbusTransferData)
        {
            if (modbusTransferData.DeviceAddress == 0)
                OnDataReceived(modbusTransferData);

            if (modbusTransferData.DeviceAddress == MasterModuleProcess.Identifier)
                OnDataReceived(modbusTransferData);
        }

        public void ReadCoilsRequest(ushort startAddress, ushort numOfCoilsToRead)
        {
            SendAddressedRequest(
                command: ModbusCommand.ReadCoils, 
                address: startAddress, 
                data:    BitConverter.GetBytes(numOfCoilsToRead).Reverse().ToArray());
        }

        public void ReadInputsRequest(ushort startAddress, ushort numOfInputsToRead)
        {
            SendAddressedRequest(
                command: ModbusCommand.ReadInputs,
                address: startAddress,
                data: BitConverter.GetBytes(numOfInputsToRead).Reverse().ToArray());
        }

        public void ReadHoldingRegistersRequest(ushort startAddress, ushort numOfRegistersToRead)
        {
            SendAddressedRequest(
                command: ModbusCommand.ReadHoldingRegisters,
                address: startAddress,
                data: BitConverter.GetBytes(numOfRegistersToRead).Reverse().ToArray());
        }

        public void ReadInputRegistersRequest(ushort startAddress, ushort numOfRegistersToRead)
        {
            SendAddressedRequest(
                command: ModbusCommand.ReadInputRegisters,
                address: startAddress,
                data: BitConverter.GetBytes(numOfRegistersToRead).Reverse().ToArray());
        }

        public void WriteSingleCoilRequest(ushort address, bool status)
        {
            SendAddressedRequest(
                command: ModbusCommand.WriteSingleCoil,
                address: address,
                data: BitConverter.GetBytes((ushort)(status ? 0x00FF : 0x0000)));
        }

        public void WriteSingleRegisterRequest(ushort address, ushort dataToWrite)
        {
            SendAddressedRequest(
                command: ModbusCommand.WriteSingleRegister,
                address: address,
                data: BitConverter.GetBytes(dataToWrite).Reverse().ToArray());
        }

        public void WriteMultipleCoilsRequest(ushort startAddress, BitArray data)
        {
            byte numOfDataBytes = (byte) Math.Ceiling(data.Length/8.0f);
            var newData = new byte[2 + 1 + numOfDataBytes];

            BitConverter.GetBytes((ushort)data.Length).Reverse().ToArray().CopyTo(newData, 0);
            newData[2] = numOfDataBytes;
            data.ToByteArray().CopyTo(newData, 3);

            SendAddressedRequest(
                command: ModbusCommand.WriteMultipleCoils,
                address: startAddress, 
                data: newData);
        }

        public void WriteMultipleRegistersRequest(ushort startAddress, ushort[] data)
        {
            byte numOfDataBytes = (byte) (data.Length * 2);
            var newData = new byte[2 + 1 + numOfDataBytes];

            BitConverter.GetBytes((ushort)data.Length).Reverse().ToArray().CopyTo(newData, 0);
            BitConverter.GetBytes(numOfDataBytes).CopyTo(newData, 2);

            var tempByte = new byte[numOfDataBytes];
            Buffer.BlockCopy(data, 0, tempByte, 0, tempByte.Length);
            tempByte.CopyTo(newData, 3);

            SendAddressedRequest(
                command: ModbusCommand.WriteMultipleRegisters,
                address: startAddress,
                data: newData);
        }

        private void SendAddressedRequest(ModbusCommand command, ushort address, byte[] data)
        {
            var newData = new byte[data.Length + 2];
            BitConverter.GetBytes(address).Reverse().ToArray().CopyTo(newData, 0);
            data.CopyTo(newData, 2);

            SendRequest(command, newData);
        }

        private void SendRequest(ModbusCommand command, byte[] data)
        {
            if (data == null) data = new byte[0];

            _modbusTransferManager.SendAsMaster(new ModbusTransferData(
                SlaveModuleProcess.Identifier, command, data));
        }

        protected abstract void OnDataReceived(ModbusTransferData modbusTransferData);
    }
}
