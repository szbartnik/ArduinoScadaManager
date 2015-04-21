using System.Windows.Controls;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;

namespace ArduinoScadaManager.Common.Core
{
    public abstract class SlaveModuleProcessBase
    {
        private readonly ICoreManager _manager;
        public abstract UserControl View { get; set; }
        public RelayCommand RemoveSlaveModuleCommand { get; set; }

        protected SlaveModuleProcessBase(ICoreManager manager)
        {
            _manager = manager;
            RemoveSlaveModuleCommand = new RelayCommand(() => _manager.RemoveSlaveModule(this));
        }

        public abstract UserControl GetScadaPanelOfSlaveModule(IMasterModuleProcess scadaModuleProcess);
    }
}