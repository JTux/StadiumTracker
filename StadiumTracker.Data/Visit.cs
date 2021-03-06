﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Data
{
    public class Visit
    {
        [Key]
        public int VisitId { get; set; }

        [ForeignKey(nameof(Visitor))]
        public int VisitorId { get; set; }
        public virtual Visitor Visitor { get; set; }

        [ForeignKey(nameof(Park))]
        public int ParkId { get; set; }
        public virtual Park Park { get; set; }

        [ForeignKey(nameof(HomeTeam))]
        public int HomeTeamId { get; set; }
        public virtual Team HomeTeam { get; set; }

        [ForeignKey(nameof(AwayTeam))]
        public int AwayTeamId { get; set; }
        public virtual Team AwayTeam { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        [Required]
        public bool GotPin { get; set; }

        [Required]
        public bool GotPhoto { get; set; }
    }
}