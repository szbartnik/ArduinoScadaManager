using System.Windows.Controls;

namespace ArduinoScadaManager.Common.Core
{
    public interface ISlaveModule
    {
        string Name { get; }
        ISlaveModuleProcess GetSlaveModuleProcess(ICoreManager manager);
    }

    public interface ISlaveModuleProcess
    {
        UserControl GetSlaveModuleDevicePanelView();
    }
}