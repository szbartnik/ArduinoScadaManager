using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Gui.Core;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, ICoreManager, ILogger
    {
        [ImportMany(typeof(ISlaveModule))]
        public List<ISlaveModule> SlaveModules { get; private set; }

        public ISlaveModule SelectedSlaveModuleToAdd { get; set; }

        public IModbusTransferManager ModbusTransferManager { get; private set; }
        public ObservableCollection<IMasterModuleProcess> ActiveMasterScadaDevices { get; private set; }
        public ObservableCollection<SlaveModuleProcessBase> ActiveSlaveDevices { get; private set; }

        public string OutputTextBoxContent
        {
            get { return _outputTextBoxContent; }
            set
            {
                _outputTextBoxContent = value;
                OnPropertyChanged();
            }
        }
        private string _outputTextBoxContent;

        public MainWindowViewModel(CompositionContainer compositionContainer)
        {
            compositionContainer.ComposeParts(this);
            InitializeCommands();
            ModbusTransferManager = new ModbusTransferManager(this);

            ActiveMasterScadaDevices = new ObservableCollection<IMasterModuleProcess>();
            ActiveSlaveDevices = new ObservableCollection<SlaveModuleProcessBase>();
        }

        private void AddNewSlaveModule(ISlaveModule slaveModuleToAdd)
        {
            var addedSlaveModule = slaveModuleToAdd.GetSlaveModuleProcess(this);
            ActiveSlaveDevices.Add(addedSlaveModule);
            OnSlaveModuleAdded(addedSlaveModule);
        }

        public void RemoveSlaveModule(SlaveModuleProcessBase slaveModuleToDelete)
        {
            ActiveSlaveDevices.Remove(slaveModuleToDelete);
            OnSlaveModuleRemoved(slaveModuleToDelete);
        }

        private void AddNewScadaModule()
        {
            ActiveMasterScadaDevices.Add(new MasterModuleProcess(this));
        }

        public void WriteDebug(string content)
        {
            OutputTextBoxContent += content + Environment.NewLine;
        }

        public void RemoveScadaModule(IMasterModuleProcess jakasKlasa)
        {
            ActiveMasterScadaDevices.Remove(jakasKlasa);
        }

        public int GenerateSlaveModuleIdentifier()
        {
            for (int i = 1;; i++)
            {
                if (ActiveSlaveDevices.All(x => x.Identifier != i))
                    return i;
            }
        }

        public int GenerateMasterModuleIdentifier()
        {
            for (int i = 1; ; i++)
            {
                if (ActiveMasterScadaDevices.All(x => x.Identifier != i))
                    return i;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            ModbusTransferManager.Dispose();
        }

        ~MainWindowViewModel()
        {
            Dispose();
        }
    }
}
