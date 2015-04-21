using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;

namespace ArduinoScadaManager.Common.ViewModels
{
    public abstract class SlaveModuleScadaPanelViewModelBase : ViewModelBase
    {
        protected readonly SlaveModuleProcessBase SlaveModuleProcessBase;
        protected readonly IMasterModuleProcess ScadaModuleProcess;

        public UserControl View { get; set; }

        protected SlaveModuleScadaPanelViewModelBase(
            IMasterModuleProcess scadaModuleProcess, 
            SlaveModuleProcessBase slaveModuleProcessBase)
        {
            SlaveModuleProcessBase = slaveModuleProcessBase;
            ScadaModuleProcess = scadaModuleProcess;
        }
    }
}
