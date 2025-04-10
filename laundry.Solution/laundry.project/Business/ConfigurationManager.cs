using laundry.project.Entities;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace laundry.project.Business
{
    internal class ConfigurationManager
    {
        public List<Machine> SetConfig(string filePath)
        {
            List<Machine> machines = new List<Machine>();
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] lineItem = line.Split(';');
                        Machine machine = new Machine();
                        machine.IdMachine = lineItem[1];
                        machine.Input = Convert.ToInt16(lineItem[0]);
                        machine.CurrentState = MachineState.A;
                        Structure.MapInputMachine.Add(machine.Input, machine.IdMachine);
                        Structure.MapInputTension.Add(machine.Input, 0);
                        machines.Add(machine);
                        if (lineItem.Length <= 2)
                            return machines;
                        List<Cycle> cycles = new List<Cycle>();
                        for (int indice = 2; indice < lineItem.Length; indice = indice + 2)
                        {
                            Cycle cycle = new Cycle();
                            cycle.NameCycle = lineItem[indice];
                            cycle.DureeCycle = Convert.ToInt32(lineItem[indice + 1]);
                            cycles.Add(cycle);
                        }
                        machine.Cycles = cycles;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite : " + ex.Message);
            }
            return machines;
        }
    }
}

