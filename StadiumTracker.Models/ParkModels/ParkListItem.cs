using StadiumTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.ParkModels
{
    public class ParkListItem
    {
        public int ParkId { get; set; }
        public string ParkName { get; set; }
        public string CityName { get; set; }
        public bool IsVisited { get; set; }
        public bool HasPin { get; set; }
        public bool HasPhoto { get; set; }
        public string TeamName { get; set; }
    }
}
