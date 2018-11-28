using StadiumTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.TeamModels
{
    public class TeamDetail
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public Guid OwnerId { get; set; }
        public League League { get; set; }
    }
}
