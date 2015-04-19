using System;
using ArduinoScadaManager.Common.Core;

namespace ArduinoScadaManager.Gui.Core
{
    public class CoreManager : ICoreManager
    {
        public event Action<SlaveModuleProcessBase> RemovingSlaveModule;

        public void RemoveSlaveModule(SlaveModuleProcessBase slaveModuleProcessBase)
        {
            if (RemovingSlaveModule != null)
                RemovingSlaveModule(slaveModuleProcessBase);
        }
    }
}