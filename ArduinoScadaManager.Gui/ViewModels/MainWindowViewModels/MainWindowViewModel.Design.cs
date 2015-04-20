using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.ViewModels.ScadaModuleProcessViewModel;
using ArduinoScadaManager.Gui.Core;

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
                get { return "Sample slave module"; }
            }

            public SlaveModuleProcessBase GetSlaveModuleProcess(ICoreManager manager)
            {
                return new DesignSlaveModuleProcess();
            }
        }

        class DesignSlaveModuleProcess : SlaveModuleProcessBase
        {
            private readonly UserControl _sampleUserControl;

            public DesignSlaveModuleProcess()
                :base(new MainWindowViewModel())
            {
                _sampleUserControl = new UserControl
                {
                    Width  = 300,
                    Height = 200,
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    BorderThickness = new Thickness(1),
                };
            }

            public override UserControl SlaveModuleDevicePanelView
            {
                get { return _sampleUserControl; }
                set { }
            }

            public override UserControl GetScadaPanelViewOfSlaveModule(ScadaModuleProcess scadaModuleProcess)
            {
                throw new NotImplementedException();
            }
        }
    }
}
