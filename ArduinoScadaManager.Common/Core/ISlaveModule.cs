namespace ArduinoScadaManager.Common.Core
{
    public interface ISlaveModule
    {
        string Name { get; }
        SlaveModuleProcessBase GetSlaveModuleProcess(ICoreManager manager);
        
    }
}