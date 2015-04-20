using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ArduinoScadaManager.Common.ViewModels.ScadaModuleProcessViewModel;

namespace ArduinoScadaManager.Common.Core
{
    public interface ICoreManager
    {
        event Action<SlaveModuleProcessBase> SlaveModuleAdded;
        ObservableCollection<ScadaModuleProcess> ActiveMasterScadaPanels { get; }
        ObservableCollection<SlaveModuleProcessBase> ActiveSlaveDevices { get; }
        void RemoveSlaveModule(SlaveModuleProcessBase slaveModuleProcessBase);
        void RemoveScadaModule(ScadaModuleProcess scadaModuleProcess);
    }
}