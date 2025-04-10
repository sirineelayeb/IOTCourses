using laundry.project.Entities;
using laundry.project.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laundry.project.Infrastructure
{
    internal class ConsoleSender : ISender
    {
        public void SendMessage(Message message)
        {
            ConsoleColor color=ConsoleColor.White;
            if (message.State==MachineState.D)
                color= ConsoleColor.Cyan;
            if (message.State == MachineState.A)
                color = ConsoleColor.Yellow;
            if (message.State == MachineState.C)
                color = ConsoleColor.Red;

            $"Message :{message.IdMachine}-{message.Date}-{message.State.ToString()}".WriteLineRight(color);
        }
    }
}
