using System.ComponentModel.Composition;
using ArduinoScadaManager.Common.Core;

namespace WaterPumpModule.Core
{
    [Export(typeof(ISlaveModule))]
    public class SlaveModule : ISlaveModule
    {
        public string Name
        {
            get { return "Water pump module"; }
        }

        public SlaveModuleProcessBase GetSlaveModuleProcess(ICoreManager manager)
        {
            return new WaterPumpSlaveModuleProcess(manager);
        }
    }
}
