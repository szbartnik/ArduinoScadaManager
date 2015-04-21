using ArduinoScadaManager.Common.Core;
using ArduinoScadaManager.Common.Interfaces;

namespace ArduinoScadaManager.Gui.Core
{
    public class ScadaPanel
    {
        private readonly ICoreManager _manager;

        public ScadaPanel(ICoreManager manager)
        {
            _manager = manager;
        }
    }
}