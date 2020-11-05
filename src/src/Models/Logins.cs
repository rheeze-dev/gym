using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Logins
    {
        public int Id { get; set; }
        public int MembersId { get; set; }
        public string FullName { get; set; }
        public DateTime Timein { get; set; }
        public DateTime? Timeout { get; set; }
    }
}
