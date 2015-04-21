using System;
using ArduinoScadaManager.Common.Core;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public partial class MainWindowViewModel
    {
        public event Action<SlaveModuleProcessBase> SlaveModuleAdded;
        public event Action<SlaveModuleProcessBase> SlaveModuleRemoved;

        public event Action<SlaveModuleProcessBase> MasterModuleAdded;
        public event Action<SlaveModuleProcessBase> MasterModuleRemoved;

        protected virtual void OnSlaveModuleAdded(SlaveModuleProcessBase obj)
        {
            Action<SlaveModuleProcessBase> handler = SlaveModuleAdded;
            if (handler != null) handler(obj);
        }

        protected virtual void OnSlaveModuleRemoved(SlaveModuleProcessBase obj)
        {
            Action<SlaveModuleProcessBase> handler = SlaveModuleRemoved;
            if (handler != null) handler(obj);
        }
    }
}
