using System;
using ArduinoScadaManager.Common.Core;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public partial class MainWindowViewModel
    {
        public event Action<SlaveModuleProcessBase> SlaveModuleAdded;
        public event Action<SlaveModuleProcessBase> ScadaModuleAdded;

        protected virtual void OnSlaveModuleAdded(SlaveModuleProcessBase obj)
        {
            Action<SlaveModuleProcessBase> handler = SlaveModuleAdded;
            if (handler != null) handler(obj);
        }
    }
}
