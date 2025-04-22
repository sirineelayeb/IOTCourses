using System;
using System.Timers;
namespace laundry.project.Entities
{
    internal class Machine
    {
        string _idMachine;
        int _input;
        MachineState _currentState;
        List<Cycle> _cycles;
        System.Timers.Timer _timer_timer;
        
        public string IdMachine { get => _idMachine; set => _idMachine = value; }
        public int Input { get => _input; set => _input = value; }
        public System.Timers.Timer Timer_timer { get => _timer_timer; set => _timer_timer = value; }
        internal List<Cycle> Cycles { get => _cycles; set => _cycles = value; }
        internal MachineState CurrentState { get => _currentState; set => _currentState = value; }
    }
}
