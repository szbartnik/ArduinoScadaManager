using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ImportMany(typeof(ISlaveModule))]
        public List<ISlaveModule> SlaveModules { get; private set; }

        public ISlaveModule SelectedSlaveModuleToAdd { get; set; }

        public ObservableCollection<UserControl> ActiveMasterScadaPanels { get; private set; }
        public ObservableCollection<UserControl> ActiveSlaveDevicePanels { get; private set; }

        private readonly CoreManager _manager = new CoreManager();

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
        }

        private void AddNewMaster()
        {
            throw new System.NotImplementedException();
        }

        private void AddNewSlave(ISlaveModule slaveModuleToAdd)
        {
            var impl = slaveModuleToAdd.GetInterfaceImplementation(_manager);
            impl.
        }
    }

    public class CoreManager : ICoreManager
    {
    }
}
