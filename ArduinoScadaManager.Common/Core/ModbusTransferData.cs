namespace ArduinoScadaManager.Common.Core
{
    public class ModbusTransferData
    {
        public byte DeviceAddress { get; set; }
        public byte CommandId { get; set; }
        public byte[] Data { get; set; }
    }
}