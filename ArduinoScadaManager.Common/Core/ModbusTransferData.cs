using System;
using ArduinoScadaManager.Common.Infrastructure;

namespace ArduinoScadaManager.Common.Core
{
    public class ModbusTransferData
    {
        private const string FrameParsePattern = @"^:(?<addr>[0-9a-fA-F]{1})" + // Address of device 
                                                 @"(?<cmd>[0-9a-fA-F]{1})" +    // Command ID
                                                 @"(?<data>[0-9a-fA-F]*)" +     // N bytes of data
                                                 @"(?<crc>[0-9a-fA-F]{2})";     // CRC

        public byte DeviceAddress { get; private set; }
        public byte CommandId { get; private set; }
        public byte[] Data { get; private set; }
        public ushort Crc { get; private set; }

        public bool IsCrcValid
        {
            get
            {
                var computed = ComputeCrc();
                return Crc == computed;
            }
        }

        private ushort ComputeCrc()
        {
            byte[] newArray = new byte[Data.Length + 2];
            newArray[0] = DeviceAddress;
            newArray[1] = CommandId;
            Data.CopyTo(newArray, 2);

            return CrcHelper.ComputeCrc(newArray);
        }

        public ModbusTransferData(string encodedTransferData)
        {
            DecodeTransferData(encodedTransferData);
        }

        public ModbusTransferData(byte address, byte commandId, byte[] data)
        {
            DeviceAddress = address;
            CommandId = commandId;
            Data = data;
            ComputeCrc();
        }

        public string EncodeTransferData()
        {
            return "";
        }

        private void DecodeTransferData(string encodedTransferData)
        {
            throw new System.NotImplementedException();
        }
    }
}