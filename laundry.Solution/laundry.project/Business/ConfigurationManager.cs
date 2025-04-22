using laundry.project.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace laundry.project.Business
{
    // This class is responsible for managing the configuration of the machines
    internal class ConfigurationManager
    {
        // This method reads the machine configuration from a specified file path and returns a list of machines.
        public List<Machine> SetConfig(string filePath)
        {
            List<Machine> machines = new List<Machine>();

            try
            {
                // Open the file for reading
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line;

                    // Read each line in the file
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Split the line by the semicolon to get the different parts
                        string[] lineItems = line.Split(';');
                        Machine machine = new Machine
                        {
                            // Set the input and ID for the machine
                            Input = Convert.ToInt16(lineItems[0]),
                            IdMachine = lineItems[1],
                            // Set the default state of the machine to state 'Stopped'
                            CurrentState = MachineState.Stopped
                        };

                        // Add the input and machine ID mapping to Structure.MapInputMachine
                        Structure.MapInputMachine.Add(machine.Input, machine.IdMachine);
                        // Set the initial input tension to 0
                        Structure.MapInputTension.Add(machine.Input, 0);

                        // If no cycle information is provided (only Input and IdMachine)
                        if (lineItems.Length <= 2)
                        {
                            // Add the machine to the list and continue to the next line
                            machines.Add(machine);
                            continue;
                        }

                        // If cycle information is provided, read the cycles
                        List<Cycle> cycles = new List<Cycle>();
                        for (int i = 2; i < lineItems.Length; i += 2)
                        {
                            // Create a new cycle object for each pair of cycle name and duration
                            Cycle cycle = new Cycle
                            {
                                NameCycle = lineItems[i], // Cycle name
                                DurationCycle = Convert.ToInt32(lineItems[i + 1]) // Cycle duration
                            };
                            cycles.Add(cycle);
                        }

                        // Assign the cycles to the machine
                        machine.Cycles = cycles;
                        // Add the machine to the list
                        machines.Add(machine);
                    }
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, display an error message
                Console.WriteLine("An error occurred while reading the configuration: " + ex.Message);
            }
            return machines;
        }
    }
}
