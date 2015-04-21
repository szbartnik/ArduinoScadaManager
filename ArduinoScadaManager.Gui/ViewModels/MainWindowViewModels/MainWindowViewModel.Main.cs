using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.Interfaces;
using ArduinoScadaManager.Gui.Core;
using PrimS.Telnet;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, ICoreManager
    {
        [ImportMany(typeof(ISlaveModule))]
        public List<ISlaveModule> SlaveModules { get; private set; }

        public ISlaveModule SelectedSlaveModuleToAdd { get; set; }

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

        private Client client;

        private async void AddNewScadaModule()
        {
            //if (client == null || !client.IsConnected)
            //    client = new Client("192.168.1.2", 23, new CancellationToken());
            //client.WriteLine("dupa");
            //WriteDebug(await client.ReadAsync(TimeSpan.FromSeconds(1)));

            ActiveMasterScadaDevices.Add(new MasterModuleProcess(this));
        }

        private void WriteDebug(string content)
        {
            OutputTextBoxContent += content;
        }

        public void RemoveScadaModule(IMasterModuleProcess jakasKlasa)
        {
            ActiveMasterScadaDevices.Remove(jakasKlasa);
        }

        public int GenerateSlaveModuleIdentifier()
        {
            for (int i = 0;; i++)
            {
                if (ActiveSlaveDevices.All(x => x.Identifier != i))
                    return i;
            }
        }

        public int GenerateMasterModuleIdentifier()
        {
            for (int i = 0; ; i++)
            {
                if (ActiveMasterScadaDevices.All(x => x.Identifier != i))
                    return i;
            }
        }
    }
}
