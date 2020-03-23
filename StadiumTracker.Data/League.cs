using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Data
{
    public class League
    {
        [Key]
        public int LeagueId { get; set; }

        [Required]
        public string LeagueName { get; set; }

        public virtual List<Team> Teams { get; set; } = new List<Team>();
    }
}
