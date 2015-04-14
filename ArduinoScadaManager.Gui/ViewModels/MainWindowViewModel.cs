using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Infrastructure;

namespace ArduinoScadaManager.Gui.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        [ImportMany(typeof(ISlaveModule))]
        public ObservableCollection<ISlaveModule> SlaveModules { get; set; }    

        public MainWindowViewModel(CompositionContainer compositionContainer)
        {
            compositionContainer.ComposeParts(this);
        }
    }
}
