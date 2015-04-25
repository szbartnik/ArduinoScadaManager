namespace ArduinoScadaManager.Common.Core
{
    public class ModbusTransferData
    {
        public byte DeviceAddress { get; set; }
        public byte CommandId { get; set; }
        public byte[] Data { get; set; }

        public ModbusTransferData(string decodedTransferData)
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