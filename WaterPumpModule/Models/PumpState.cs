using System;

namespace WaterPumpModule.Models
{
    [Flags]
    public enum PumpState
    {
        Starting = 1 << 1,
        Running  = 1 << 2,
        Stopping = 1 << 3,
        Stopped  = 1 << 4,
        Failure  = 1 << 5,
    }
}
