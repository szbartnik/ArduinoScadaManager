using System;
using System.Collections;
using System.Linq;
using System.Windows.Documents;
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

            Coils = new BitArray(256);
            Inputs = new BitArray(256);
            HoldingRegisters = new ushort[256];
            InputRegisters = new ushort[256];
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

            SendResponse(new ModbusTransferData(
                _slaveModuleProcess.Identifier, command, newData));
        }

        private void SendResponse(ModbusTransferData modbusTransferData)
        {
            _modbusTransferManager.SendAsSlave(modbusTransferData);
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
                    WriteSingleCoil(modbusTransferData);
                    break;
                case ModbusCommand.WriteSingleRegister:
                    WriteSingleRegister(modbusTransferData);
                    break;
                case ModbusCommand.WriteMultipleCoils:
                    WriteMultipleCoils(modbusTransferData);
                    break;
                case ModbusCommand.WriteMultipleRegisters:
                    WriteMultipleRegisters(modbusTransferData);
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
                startAddress, 
                numOfCoilsOrInputsToRead);

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

        private void WriteSingleCoil(ModbusTransferData modbusTransferData)
        {
            var data = modbusTransferData.Data;

            var address = data.GetUint16(0);
            var booleanData = data.GetUint16(2);

            switch (booleanData)
            {
                case 0xFF00:
                    Coils[address] = true;
                    break;
                case 0x0000:
                    Coils[address] = false;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
            SendResponse(modbusTransferData);
        }

        private void WriteSingleRegister(ModbusTransferData modbusTransferData)
        {
            var data = modbusTransferData.Data;

            var address = data.GetUint16(0);
            var valueToWrite = data.GetUint16(2);

            HoldingRegisters[address] = valueToWrite;

            SendResponse(modbusTransferData);
        }

        private void WriteMultipleCoils(ModbusTransferData modbusTransferData)
        {
            var data = modbusTransferData.Data;

            var address = data.GetUint16(0);
            var numOfCoilsToWrite = data.GetUint16(2);
            var numOfBytes = data[4];

            var coilsData = new byte[numOfBytes];
            Buffer.BlockCopy(data, 5, coilsData, 0, coilsData.Length);

            var bitArray = new BitArray(coilsData);
            for (int i = 0; i < numOfCoilsToWrite; i++)
            {
                Coils[address + i] = bitArray[i];
            }

            var newData = new byte[4];
            Buffer.BlockCopy(modbusTransferData.Data, 2, newData, 0, newData.Length);

            SendResponse(new ModbusTransferData(
                modbusTransferData.DeviceAddress,
                modbusTransferData.Command,
                newData));
        }

        private void WriteMultipleRegisters(ModbusTransferData modbusTransferData)
        {
            var data = modbusTransferData.Data;

            var address = data.GetUint16(0);
            var numOfRegistersToWrite = data.GetUint16(2);
            var numOfBytes = data[4];

            var registersData = new ushort[numOfRegistersToWrite];
            Buffer.BlockCopy(data, 5, registersData, 0, numOfBytes);

            for (int i = 0; i < numOfRegistersToWrite; i++)
            {
                HoldingRegisters[address + i] = registersData[i];
            }

            var newData = new byte[4];
            Buffer.BlockCopy(modbusTransferData.Data, 2, newData, 0, newData.Length);

            SendResponse(new ModbusTransferData(
                modbusTransferData.DeviceAddress,
                modbusTransferData.Command,
                newData));
        }
    }
}
