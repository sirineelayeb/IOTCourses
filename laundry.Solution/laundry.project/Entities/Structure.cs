using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laundry.project.Entities
{
    public enum MachineState
    {
        [Description("Arrêt")]        // Stopped (Machine is off)
        Stopped,

        [Description("Déclenchée")]   // Started (Machine is started)
        Started,

        [Description("Cycle de machine")] // In Cycle (Machine is in a cycle)
        InCycle,

        [Description("Indéfini")]     // Undefined (Machine state is undefined)
        Undefined
    }

    public static class Structure {
        public static Dictionary<int, string> MapInputMachine = new Dictionary<int, string>();
        public static Dictionary<int, int> MapInputTension = new Dictionary<int, int>();
       
    }
}
