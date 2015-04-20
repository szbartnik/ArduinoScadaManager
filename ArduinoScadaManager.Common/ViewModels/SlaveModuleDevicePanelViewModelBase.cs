using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;

namespace ArduinoScadaManager.Common.ViewModels
{
    public abstract class SlaveModuleDevicePanelViewModelBase : ViewModelBase
    {
        protected readonly ICoreManager Manager;

        protected SlaveModuleDevicePanelViewModelBase(ICoreManager manager)
        {
            Manager = manager;
        }
    }
}
