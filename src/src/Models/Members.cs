using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Members
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string School { get; set; }
        public string MedicalCondition { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public int LockerNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsStudent { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public bool IsTimeout { get; set; }
        public int AmountPaid { get; set; }
        public int Plan { get; set; }

    }
}
