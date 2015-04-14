using System.Windows.Controls;

namespace ArduinoScadaManager.Common.Core
{
    public interface ISlaveModule
    {
        UserControl GetModuleDevicePanelView();
        UserControl GetModuleScadaPanelView();
    }
}