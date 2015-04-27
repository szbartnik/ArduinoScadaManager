using System;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Common.ViewModels;

namespace ArduinoScadaManager.Common.Core
{
    public abstract class SlaveModuleProcessBase : ISlaveModuleProcess
    {
        protected readonly ICoreManager CoreManager;

        public UserControl View
        {
            get { return _view ?? (_view = GetDevicePanelView()); }
        }
        private UserControl _view;

        public RelayCommand RemoveSlaveModuleCommand { get; set; }

        public string Name { get; private set; }
        public byte Identifier { get; private set; }

        protected SlaveModuleProcessBase(ICoreManager manager, string name)
        {
            CoreManager = manager;
            RemoveSlaveModuleCommand = new RelayCommand(() => CoreManager.RemoveSlaveModule(this));
            Identifier = CoreManager.GenerateSlaveModuleIdentifier();
            Name = name;
        }

        public SlaveModuleScadaPanelViewModelBase GetScadaPanelOfSlaveModule(IMasterModuleProcess masterModuleProcess)
        {
            return GetScadaPanelOfSlaveModule(masterModuleProcess, this);
        }

        protected abstract SlaveModuleScadaPanelViewModelBase GetScadaPanelOfSlaveModule(
            IMasterModuleProcess masterModuleProcess, 
            ISlaveModuleProcess slaveModuleProcessBase);

        protected abstract UserControl GetDevicePanelView();
    }
}