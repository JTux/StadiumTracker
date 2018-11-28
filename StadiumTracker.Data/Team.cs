using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public int LeagueId { get; set; }
        public virtual League League { get; set; }
    }
}
