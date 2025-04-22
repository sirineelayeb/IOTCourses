using laundry.project.Entities;
using laundry.project.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laundry.project.Infrastructure
{
    internal class ConsoleMessageSender : ISender
    {
        public void SendMessage(Message message)
        {
            ConsoleColor color = message.State switch
            {
                MachineState.Started => ConsoleColor.Cyan,
                MachineState.Stopped => ConsoleColor.Yellow,
                MachineState.InCycle => ConsoleColor.Red,
                _ => ConsoleColor.White
            };
            Console.ForegroundColor = color;
            Console.WriteLine($"Message : {message.IdMachine} - {message.Date} - {message.State}");
            Console.ResetColor();
        }
    }
}