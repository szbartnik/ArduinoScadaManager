using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;
using ArduinoScadaManager.Common.ViewModels.ScadaModuleProcessViewModel;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, ICoreManager
    {
        [ImportMany(typeof(ISlaveModule))]
        public List<ISlaveModule> SlaveModules { get; private set; }

        public ISlaveModule SelectedSlaveModuleToAdd { get; set; }

        public ObservableCollection<ScadaModuleProcess> ActiveMasterScadaPanels { get; private set; }
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

            ActiveMasterScadaPanels = new ObservableCollection<ScadaModuleProcess>();
            ActiveSlaveDevices = new ObservableCollection<SlaveModuleProcessBase>();
        }

        private void AddNewSlaveModule(ISlaveModule slaveModuleToAdd)
        {
            var addedSlaveModule = slaveModuleToAdd.GetSlaveModuleProcess(this);
            ActiveSlaveDevices.Add(addedSlaveModule);
        }

        public void RemoveSlaveModule(SlaveModuleProcessBase slaveModuleToDelete)
        {
            ActiveSlaveDevices.Remove(slaveModuleToDelete);
        }

        private void AddNewScadaModule()
        {
            ActiveMasterScadaPanels.Add(new ScadaModuleProcess(this));
        }

        public void RemoveScadaModule(ScadaModuleProcess scadaModuleProcess)
        {
            //ActiveMasterScadaPanels.Remove()
        }
    }
}
