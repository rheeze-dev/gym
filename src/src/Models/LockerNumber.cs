using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class LockerNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public bool IsOccupied { get; set; }
    }
}
