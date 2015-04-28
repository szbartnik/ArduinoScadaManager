using System;
using ArduinoScadaManager.Common.Core;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public sealed partial class MainWindowViewModel
    {
        public event Action<SlaveModuleProcessBase> SlaveModuleAdded;
        public event Action<SlaveModuleProcessBase> SlaveModuleRemoved;

        private void OnSlaveModuleAdded(SlaveModuleProcessBase obj)
        {
            Action<SlaveModuleProcessBase> handler = SlaveModuleAdded;
            if (handler != null) handler(obj);
        }

        private void OnSlaveModuleRemoved(SlaveModuleProcessBase obj)
        {
            Action<SlaveModuleProcessBase> handler = SlaveModuleRemoved;
            if (handler != null) handler(obj);
        }
    }
}
