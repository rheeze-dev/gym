using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class DailyCollection
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public int Amount { get; set; }
        public string Origin { get; set; }
        public int Total { get; set; }

    }
}
