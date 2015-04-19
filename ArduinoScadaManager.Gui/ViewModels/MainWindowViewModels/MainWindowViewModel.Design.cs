using System.Collections.Generic;
using System.Windows.Controls;
using ArduinoScadaManager.Common.Core;

namespace ArduinoScadaManager.Gui.ViewModels.MainWindowViewModels
{
    public partial class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            SlaveModules = new List<ISlaveModule>
            {
                new DesignSlaveModule(),
                new DesignSlaveModule(),
                new DesignSlaveModule(),
            };
        }

        class DesignSlaveModule : ISlaveModule
        {
            public string Name
            {
                get { throw new System.NotImplementedException(); }
            }

            public ISlaveModuleProcess GetSlaveModuleProcess(ICoreManager manager)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
