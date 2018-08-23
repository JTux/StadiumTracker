using StadiumTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.ParkModels
{
    public class ParkEdit
    {
        public int ParkId { get; set; }
        public string ParkName { get; set; }
        public Team HomeTeam { get; set; }
        public string CityName { get; set; }
    }
}