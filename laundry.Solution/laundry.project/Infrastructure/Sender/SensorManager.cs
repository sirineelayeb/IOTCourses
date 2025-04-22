using laundry.project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laundry.project.Infrastructure.Sender
{
    internal class SensorManager
    {
        void InitSensorMAp()
        {

        }

        int GetSensorValue(int input)
        {
            return Structure.MapInputTension[input];
        }
        public MachineState GetMachineState(int input)
        {
            int value = GetSensorValue( input);
            switch (value)
            {
                case 0:return MachineState.Stopped;
                case 5: return MachineState.Started; 
                case 10: return MachineState.InCycle; 
                default : return MachineState.Undefined; 
            }
        }
    }
}
