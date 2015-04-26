namespace ArduinoScadaManager.Common.Core
{
    public class ModbusTransferData
    {
        private const string FrameParsePattern = @"^:(?<addr>[0-9a-fA-F]{1})" +  // Address of device 
                                                 @"(?<command>[0-9a-fA-F]{1})" + // Command ID
                                                 @"(?<data>[0-9a-fA-F]*)" +   // N bytes of data
                                                 @"(?<crc>[0-9a-fA-F]{2})";  // CRC

        public byte DeviceAddress { get; set; }
        public byte CommandId { get; set; }
        public byte[] Data { get; set; }
        public ushort Crc { get; set; }

        public ModbusTransferData(string encodedTransferData)
        {
            
        }

        public ModbusTransferData(byte address, byte commandId, byte[] data)
        {
            
        }

        public string EncodeTransferData()
        {
            return "";
        }
    }
}