using laundry.project.Entities;
using laundry.project.Infrastructure;
using laundry.project.Infrastructure.Sender;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;

namespace laundry.project.Business
{
    // This class manages the state machine for monitoring and controlling the machines.
    public class StateMachineManager
    {
        // Console positions for displaying output
        private static int consoleLeft = Console.WindowWidth / 2;
        private static int consoleTop = 0;

        // Launch monitoring threads for all machines in the list
        internal static void StartStateMachines(List<Machine> machines, SensorManager sensorManager, ISender sender)
        {
            // Iterate through each machine and start a thread to monitor its state
            foreach (Machine machine in machines)
            {
                Thread monitoringThread = CreateStateMachineThread(machine, sensorManager, sender);
                monitoringThread.Start(); // Start the monitoring thread for each machine
            }
        }

        // Create and return a new thread for monitoring a machine's state
        private static Thread CreateStateMachineThread(Machine machine, SensorManager sensorManager, ISender sender)
        {
            // The thread will execute the MonitorMachineState method for this machine
            return new Thread(() => MonitorMachineState(machine, sensorManager, sender));
        }

        // Monitor the machine state and react accordingly when there is a change
        private static void MonitorMachineState(Machine machine, SensorManager sensorManager, ISender sender)
        {
            // Infinite loop to constantly monitor the machine state
            while (true)
            {
                // Get the current state of the machine using the sensor manager
                MachineState newState = sensorManager.GetMachineState(machine.Input);

                // Check if the machine's state has changed
                switch (machine.CurrentState)
                {
                    case MachineState.Stopped: 
                    case MachineState.Started: 
                    case MachineState.InCycle: 
                        if (machine.CurrentState == newState) break; // If the state hasn't changed, break out of the loop

                        // If the state has changed, send a message with the new state
                        sender.SendMessage(new Message(machine.IdMachine, DateTime.Now, newState));
                        machine.CurrentState = newState; // Update the machine's current state
                        break;
                }

                Thread.Sleep(1000); 
            }
        }

        // Starts a machine cycle using a timer
        internal static void StartCycle(Machine machine, Cycle cycle)
        {
            // Create a timer for the cycle duration (in milliseconds)
            machine.Timer_timer = new System.Timers.Timer(cycle.DurationCycle * 1000);

            // Attach an event handler for when the cycle timer elapses
            machine.Timer_timer.Elapsed += (sender, e) => OnCycleTimerElapsed(sender, e, machine);

            // Set the timer to execute only once (not repeat)
            machine.Timer_timer.AutoReset = false;
            // Enable the timer (starts the countdown)
            machine.Timer_timer.Enabled = true;
        }

        // Called when the cycle timer elapses, to reset the machine tension
        private static void OnCycleTimerElapsed(object? sender, ElapsedEventArgs e, Machine machine)
        {
            // Reset the machine's input tension value in the Structure map
            Structure.MapInputTension[machine.Input] = 5; // Set tension to 5 (default value)
        }
    }
}
