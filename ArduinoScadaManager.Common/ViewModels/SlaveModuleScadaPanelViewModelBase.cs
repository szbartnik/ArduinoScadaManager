using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;

namespace ArduinoScadaManager.Common.ViewModels
{
    public abstract class SlaveModuleScadaPanelViewModelBase : ViewModelBase
    {
        public SlaveModuleProcessBase SlaveModuleProcessBase { get; private set; }
        public IMasterModuleProcess MasterModuleProcess;

        public UserControl View { get; set; }

        protected SlaveModuleScadaPanelViewModelBase(
            IMasterModuleProcess masterModuleProcess, 
            SlaveModuleProcessBase slaveModuleProcessBase)
        {
            SlaveModuleProcessBase = slaveModuleProcessBase;
            MasterModuleProcess = masterModuleProcess;
        }
    }
}
