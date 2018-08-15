using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.Park
{
    public class ParkListItem
    {
        public int ParkId { get; set; }
        public string ParkName { get; set; }
        public string TeamName { get; set; }
        public bool IsVisited { get; set; }
    }
}
