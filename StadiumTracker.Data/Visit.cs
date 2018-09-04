using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Data
{
    public class Visit
    {
        [Key]
        public int VisitId { get; set; }

        [Required]
        public int ParkId { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        public int VisitorId { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        public virtual Park Park { get; set; }
        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }
        public virtual Visitor Visitor { get; set; }

        [Required]
        public bool GotPin { get; set; }

        [Required]
        public bool GotPhoto { get; set; }
    }
}