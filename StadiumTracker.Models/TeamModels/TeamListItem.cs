using StadiumTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.TeamModels
{
    public class TeamListItem
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public int ParkId { get; set; }
        public virtual Park Park { get; set; }

        public int LeagueId { get; set; }
        public virtual League League { get; set; }
    }
}
