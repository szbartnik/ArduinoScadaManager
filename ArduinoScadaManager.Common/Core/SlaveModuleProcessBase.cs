using System;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.ViewModels;

namespace ArduinoScadaManager.Common.Core
{
    public abstract class SlaveModuleProcessBase
    {
        public readonly ICoreManager Manager;

        public UserControl View
        {
            get { return _view ?? (_view = GetDevicePanelView()); }
        }
        private UserControl _view;

        public RelayCommand RemoveSlaveModuleCommand { get; set; }

        public string Name { get; private set; }
        public int Identifier { get; private set; }

        protected SlaveModuleProcessBase(ICoreManager manager, string name)
        {
            Manager = manager;
            RemoveSlaveModuleCommand = new RelayCommand(() => Manager.RemoveSlaveModule(this));
            Identifier = Manager.GenerateSlaveModuleIdentifier();
            Name = name;
        }

        public SlaveModuleScadaPanelViewModelBase GetScadaPanelOfSlaveModule(IMasterModuleProcess masterModuleProcess)
        {
            return GetScadaPanelOfSlaveModule(masterModuleProcess, this);
        }

        protected abstract SlaveModuleScadaPanelViewModelBase GetScadaPanelOfSlaveModule(
            IMasterModuleProcess masterModuleProcess,
            SlaveModuleProcessBase slaveModuleProcessBase);

        protected abstract UserControl GetDevicePanelView();
    }
}