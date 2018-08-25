using StadiumTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.TeamModels
{
    public class TeamCreate
    {
        [Required]
        public string TeamName { get; set; }

        public int LeagueId { get; set; }

        public virtual League League { get; set; }
    }
}
