using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Data
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        public string TeamName { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [ForeignKey(nameof(League))]
        public int LeagueId { get; set; }
        public virtual League League { get; set; }

        public virtual List<Visit> Visits { get; set; } = new List<Visit>();
    }
}
