using ArduinoScadaManager.Common.Core;

namespace ArduinoScadaManager.Common.Interfaces
{
    public interface ISlaveModule
    {
        string Name { get; }
        SlaveModuleProcessBase GetSlaveModuleProcess(ICoreManager manager);
        
    }
}