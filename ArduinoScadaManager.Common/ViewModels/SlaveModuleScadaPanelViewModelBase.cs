using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;

namespace ArduinoScadaManager.Common.ViewModels
{
    public abstract class SlaveModuleScadaPanelViewModelBase : ViewModelBase
    {
        protected readonly IMasterModuleProcess ScadaModuleProcess;

        protected SlaveModuleScadaPanelViewModelBase(IMasterModuleProcess scadaModuleProcess)
        {
            ScadaModuleProcess = scadaModuleProcess;
        }
    }
}
