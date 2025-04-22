using laundry.project.Business;
using laundry.project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace laundry.project.Presentation
{
    // Handles user interactions and displays machine configurations and status.
    class DisplayManager
    {
        // Display the machine configurations and their cycles
        public void DisplayConfiguration(List<Machine> machines, Dictionary<int, string> map)
        {
            "** Laundry Configuration **".WriteLineLeft(ConsoleColor.Yellow);
            "--------------------------------".WriteLineLeft(ConsoleColor.Yellow);

            foreach (var machine in machines)
            {
                // Machine Header
                $"[Machine ID: {machine.IdMachine}] - Input Port: {machine.Input}".WriteLineLeft(ConsoleColor.Cyan);
                $"  {"Cycle",-15} {"Duration (sec)",-15}".WriteLineLeft(ConsoleColor.DarkGray);
                $"  {"----------------",-30}".WriteLineLeft(ConsoleColor.DarkGray);

                // Machine's Cycles
                foreach (var cycle in machine.Cycles)
                {
                    $"  {cycle.NameCycle,-15} {cycle.DurationCycle,-15}".WriteLineLeft(ConsoleColor.White);
                }

                "".WriteLineLeft(ConsoleColor.White); 
            }

            "** Input-Machine Mapping **".WriteLineLeft(ConsoleColor.Yellow);
            foreach (var (key, value) in Structure.MapInputMachine)
            {
                $"{key} <-> {value}".WriteLineLeft(ConsoleColor.Yellow);
            }

            "".WriteLineLeft(ConsoleColor.Yellow);
        }

        // Main menu to interact with the user
        public void DisplayMainMenu(List<Machine> machines, Dictionary<int, string> map)
        {
            string? choice;
            do
            {
                "** MAIN MENU **".WriteLineLeft(ConsoleColor.White);
                "1 - Stop a Machine".WriteLineLeft(ConsoleColor.White);
                "2 - Start a Machine".WriteLineLeft(ConsoleColor.White);
                "3 - Run a Cycle".WriteLineLeft(ConsoleColor.White);

                "Your Choice: ".WriteLeft(ConsoleColor.Green);
                string? option = Utility.ReadLineLeft(ConsoleColor.Green);

                switch (option)
                {
                    case "1": ShowStopMachineMenu(machines, map); break;
                    case "2": ShowStartMachineMenu(machines, map); break;
                    case "3": ShowCycleMenu(machines, map); break;
                }

                "Continue (press q to quit): ".WriteLeft(ConsoleColor.Green);
                choice = Utility.ReadLineLeft(ConsoleColor.Green);
                Console.ResetColor();
            } while (choice != "q");
        }

        // Show menu to run a cycle on a running machine
        private void ShowCycleMenu(List<Machine> machines, Dictionary<int, string> map)
        {
            var activeMachines = machines.Where(m => m.CurrentState == MachineState.Started).ToList();

            if (!activeMachines.Any())
            {
                "No machines are currently ON.".WriteLeft(ConsoleColor.Red);
                return;
            }

            "Available Machines:".WriteLineLeft(ConsoleColor.White);
            foreach (var m in activeMachines)
                $"Machine ID: {m.IdMachine}".WriteLeft(ConsoleColor.White);

            "Select Machine ID: ".WriteLeft(ConsoleColor.Green);
            string? machineId = Utility.ReadLineLeft(ConsoleColor.Green);

            var selectedMachine = machines.FirstOrDefault(m => m.IdMachine == machineId);
            if (selectedMachine != null)
            {
                "Available Cycles:".WriteLineLeft(ConsoleColor.White);
                foreach (var c in selectedMachine.Cycles)
                    $"Cycle: {c.NameCycle} | Duration: {c.DurationCycle}".WriteLeft(ConsoleColor.White);

                "Select Cycle Name: ".WriteLeft(ConsoleColor.Green);
                string? cycleName = Utility.ReadLineLeft(ConsoleColor.Green);

                var selectedCycle = selectedMachine.Cycles.FirstOrDefault(c => c.NameCycle == cycleName);
                if (selectedCycle != null)
                {
                    Structure.MapInputTension[selectedMachine.Input] = 10;
                    StateMachineManager.StartCycle(selectedMachine, selectedCycle);
                }
            }
        }

        // Show menu to start a machine
        public void ShowStartMachineMenu(List<Machine> machines, Dictionary<int, string> map)
        {
            var stoppedMachines = machines.Where(m => m.CurrentState == MachineState.Stopped).ToList();

            if (!stoppedMachines.Any())
            {
                "No machines are currently OFF.".WriteLeft(ConsoleColor.Red);
                return;
            }

            "Available Machines to Start:".WriteLineLeft(ConsoleColor.White);
            foreach (var m in stoppedMachines)
                $"Machine ID: {m.IdMachine}".WriteLeft(ConsoleColor.White);

            "Select Machine ID: ".WriteLeft(ConsoleColor.Green);
            string? machineId = Utility.ReadLineLeft(ConsoleColor.Green);

            var machineToStart = machines.FirstOrDefault(m => m.IdMachine == machineId);
            if (machineToStart != null)
            {
                Structure.MapInputTension[machineToStart.Input] = 5;
            }
        }

        // Show menu to stop a running machine
        public void ShowStopMachineMenu(List<Machine> machines, Dictionary<int, string> map)
        {
            var runningMachines = machines.Where(m => m.CurrentState == MachineState.Started).ToList();

            if (!runningMachines.Any())
            {
                "No machines are currently ON.".WriteLeft(ConsoleColor.Red);
                return;
            }

            "Available Machines to Stop:".WriteLineLeft(ConsoleColor.White);
            foreach (var m in runningMachines)
                $"Machine ID: {m.IdMachine}".WriteLeft(ConsoleColor.White);

            "Select Machine ID: ".WriteLeft(ConsoleColor.Green);
            string? machineId = Utility.ReadLineLeft(ConsoleColor.Green);

            var machineToStop = machines.FirstOrDefault(m => m.IdMachine == machineId);
            if (machineToStop != null)
            {
                Structure.MapInputTension[machineToStop.Input] = 0;
            }
        }
    }
}
