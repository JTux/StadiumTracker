using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models
{
    public class VisitorDetail
    {
        public int VisitorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TotalVisits { get; set; }
    }
}
