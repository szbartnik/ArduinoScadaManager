using System;
using System.Collections;
using System.Linq;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.Models;

namespace ArduinoScadaManager.Common.ViewModels
{
    public abstract class SlaveModuleDevicePanelViewModelBase : ViewModelBase
    {
        private readonly IModbusTransferManager _modbusTransferManager;
        private readonly SlaveModuleProcessBase _slaveModuleProcess;

        public BitArray Coils { get; private set; }
        public BitArray Inputs { get; private set; }

        public ushort[] HoldingRegisters { get; private set; }
        public ushort[] InputRegisters { get; private set; }

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

        private void SendReadCoilsOrInputsResponse(ModbusCommand command, BitArray data)
        {
            byte numOfDataBytes = (byte)Math.Ceiling(data.Length / 8.0f);
            var newData = new byte[1 + numOfDataBytes];

            newData[0] = numOfDataBytes;
            data.ToByteArray().CopyTo(newData, 1);

            SendReadResponse(command, newData);
        }

        private void SendReadHoldingOrInputRegisterResponse(ModbusCommand command, ushort[] data)
        {
            var tempByte = new byte[data.Length * 2];
            Buffer.BlockCopy(data, 0, tempByte, 0, tempByte.Length);
            SendReadResponse(command, tempByte);
        }

        private void SendReadResponse(ModbusCommand command, byte[] data)
        {
            if(data == null) data = new byte[0];

            var newData = new byte[data.Length + 1];
            data[0] = (byte) data.Length;
            data.CopyTo(newData, 1);

            _modbusTransferManager.SendAsSlave(new ModbusTransferData(
                _slaveModuleProcess.Identifier, command, newData));
        }

        private void OnDataReceived(ModbusTransferData modbusTransferData)
        {
            switch (modbusTransferData.Command)
            {
                case ModbusCommand.ReadCoils:
                    ReadCoilsOrInputs(modbusTransferData, Coils);
                    break;
                case ModbusCommand.ReadInputs:
                    ReadCoilsOrInputs(modbusTransferData, Inputs);
                    break;
                case ModbusCommand.ReadHoldingRegisters:
                    ReadHoldingOrInputRegisters(modbusTransferData, HoldingRegisters);
                    break;
                case ModbusCommand.ReadInputRegisters:
                    ReadHoldingOrInputRegisters(modbusTransferData, InputRegisters);
                    break;
                case ModbusCommand.WriteSingleCoil:
                    break;
                case ModbusCommand.WriteSingleRegister:
                    break;
                case ModbusCommand.WriteMultipleCoils:
                    break;
                case ModbusCommand.WriteMultipleRegisters:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ReadCoilsOrInputs(ModbusTransferData modbusTransferData, BitArray coilsOrInputs)
        {
            var data = modbusTransferData.Data;

            var startAddress = data.GetUint16(0);
            var numOfCoilsOrInputsToRead = data.GetUint16(2);

            var bitArray = coilsOrInputs.Get(
                (int)startAddress, 
                (int)numOfCoilsOrInputsToRead);

            SendReadCoilsOrInputsResponse(
                modbusTransferData.Command,
                bitArray);
        }

        private void ReadHoldingOrInputRegisters(ModbusTransferData modbusTransferData, ushort[] holdingOrInput)
        {
            var data = modbusTransferData.Data;

            var startAddress = data.GetUint16(0);
            var numOfHoldingOrInputRegistersToRead = data.GetUint16(2);

            var newData = new ushort[numOfHoldingOrInputRegistersToRead];
            Array.Copy(holdingOrInput, startAddress, newData, 0, numOfHoldingOrInputRegistersToRead);

            SendReadHoldingOrInputRegisterResponse(
                modbusTransferData.Command, 
                newData);
        }
    }
}
