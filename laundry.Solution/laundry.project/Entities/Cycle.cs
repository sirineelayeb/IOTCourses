using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laundry.project.Entities
{
    internal class Cycle
    {
        int _idCycle;
        string _nameCycle;
        int _dureeCycle;

        public int IdCycle { get => _idCycle; set => _idCycle = value; }
        public string NameCycle { get => _nameCycle; set => _nameCycle = value; }
        public int DureeCycle { get => _dureeCycle; set => _dureeCycle = value; }
    }
}
