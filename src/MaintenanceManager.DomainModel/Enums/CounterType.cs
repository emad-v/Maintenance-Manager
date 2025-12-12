using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceManager.DomainModel.Enums
{
    public enum CounterType
    {
        WORKING_HOURS,    // Total time the machine is powered on (includes idle time)
        PRODUCTION_HOURS, // Time spent actively processing material (cutting, drilling, etc.)
        CYCLES,           // Number of completed operation cycles (e.g., drill strokes, plasma pierces)
        STARTS,           // Number of times a motor or system was started (power-on activations)
        TOOL_CHANGES     // Number of times a tool was replaced (e.g., drill bit or cutting tool swap)
    }

}