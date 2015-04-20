using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.ViewModels.ScadaModuleProcessViewModel;

namespace ArduinoScadaManager.Common.ViewModels
{
    public abstract class SlaveModuleScadaPanelViewModelBase : ViewModelBase
    {
        protected readonly ScadaModuleProcess ScadaModuleProcess;

        protected SlaveModuleScadaPanelViewModelBase(ScadaModuleProcess scadaModuleProcess)
        {
            ScadaModuleProcess = scadaModuleProcess;
        }
    }
}
