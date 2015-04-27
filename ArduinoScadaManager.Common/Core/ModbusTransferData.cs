using System;
using System.Text.RegularExpressions;
using ArduinoScadaManager.Common.Infrastructure;

namespace ArduinoScadaManager.Common.Core
{
    public class ModbusTransferData
    {
        private const string FrameParsePattern = @":(?<addr>[0-9a-fA-F]{2})" +  // Address of device 
                                                 @"(?<cmd>[0-9a-fA-F]{2})" +    // Command ID
                                                 @"(?<data>[0-9a-fA-F]*)" +     // N bytes of data
                                                 @"(?<crc>[0-9a-fA-F]{4})\r\n"; // CRC

        public byte DeviceAddress { get; private set; }
        public byte CommandId { get; private set; }
        public byte[] Data { get; private set; }
        public ushort Crc { get; private set; }

        public bool IsCrcValid
        {
            get { return Crc == ComputeCrc(); }
        }

        private ushort ComputeCrc()
        {
            var newArray = new byte[Data.Length + 2];
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
            Crc = ComputeCrc();
        }

        public string EncodeTransferData()
        {
            var newArray = new byte[Data.Length + 4];
            newArray[0] = DeviceAddress;
            newArray[1] = CommandId;
            Data.CopyTo(newArray, 2);
            BitConverter.GetBytes(Crc).CopyTo(newArray, Data.Length + 2);

            return String.Format(":{0}\r\n", newArray.ByteArrayToHexString());
        }

        private void DecodeTransferData(string encodedTransferData)
        {
            var regex = new Regex(FrameParsePattern, RegexOptions.None);

            var match = regex.Match(encodedTransferData);
            if (!match.Success)
                throw new ArgumentException("Line could not be parsed.");

            DeviceAddress = match.Groups["addr"].Value.HexStringToByteArray()[0];
            CommandId     = match.Groups["cmd"].Value.HexStringToByteArray()[0];
            Data          = match.Groups["data"].Value.HexStringToByteArray();

            var crcBytes  = match.Groups["crc"].Value.HexStringToByteArray();;
            Crc           = BitConverter.ToUInt16(new[] {crcBytes[0], crcBytes[1]}, 0);
        }
    }
}