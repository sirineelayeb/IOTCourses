using laundry.project.Entities;
using laundry.project.Infrastructure;
using laundry.project.Infrastructure.Sender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace laundry.project.Business
{
    public class StateMachineManager
    {
        static int left= Console.WindowWidth / 2;
        static int top=0;
        static Thread CreateStateMAchineThread(Machine machine, SensorManager sensorManager, ISender sender)
        {
            Thread t = new Thread(() => CreateStateMachine(machine, sensorManager, sender));
            return t;

        }
        static void CreateStateMachine(Machine machine, SensorManager sensorManager, ISender sender)
        {

            while (true)
            {
                MachineState newState = sensorManager.GetMachineState(machine.Input);
                switch (machine.CurrentState)
                {
                    case MachineState.A:
                    case MachineState.D:
                    case MachineState.C:
                        if (machine.CurrentState == newState) break;
                        sender.SendMessage(new Message(machine.IdMachine, DateTime.Now, newState));
                        machine.CurrentState = newState;
                        break;
                }
                Thread.Sleep(1000 * 1);
            }
        }
        internal static void LancerStateMachine(List<Machine> machines, SensorManager sensorManager, ISender sender)
        {
            foreach (Machine machine in machines)
            {
                Thread t = CreateStateMAchineThread(machine, sensorManager, sender);
                t.Start();

            }
        }
        internal static void StartCycle(Machine machine,Cycle cycle)
        {
            
            machine.Timer_timer= new System.Timers.Timer(cycle.DureeCycle*1000);
            
            machine.Timer_timer.Elapsed += (sender,e ) => OnTimerElapsed(sender, e, machine);
            machine.Timer_timer.AutoReset = false; // Le timer ne se répète pas
            machine.Timer_timer.Enabled = true; // Démarrer le timer

        }
        private static void OnTimerElapsed(object? sender, ElapsedEventArgs e,Machine machine )
        {

            Structure.MapInputTension[machine.Input] = 5;
            
            
        }
    }
}
