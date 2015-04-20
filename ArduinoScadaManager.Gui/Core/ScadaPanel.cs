using ArduinoScadaManager.Common.Core;

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