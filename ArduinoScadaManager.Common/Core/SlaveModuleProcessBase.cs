using System;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.ViewModels;

namespace ArduinoScadaManager.Common.Core
{
    public abstract class SlaveModuleProcessBase
    {
        private readonly ICoreManager _manager;
        public abstract UserControl View { get; set; }
        public RelayCommand RemoveSlaveModuleCommand { get; set; }
        public int Identifier { get; private set; }

        protected SlaveModuleProcessBase(ICoreManager manager)
        {
            _manager = manager;
            RemoveSlaveModuleCommand = new RelayCommand(() => _manager.RemoveSlaveModule(this));
            Identifier = _manager.GenerateSlaveModuleIdentifier();
        }

        public SlaveModuleScadaPanelViewModelBase GetScadaPanelOfSlaveModule(IMasterModuleProcess scadaModuleProcess)
        {
            return GetScadaPanelOfSlaveModule(scadaModuleProcess, this);
        }

        protected abstract SlaveModuleScadaPanelViewModelBase GetScadaPanelOfSlaveModule(
            IMasterModuleProcess scadaModuleProcess,
            SlaveModuleProcessBase slaveModuleProcessBase);
    }
}