﻿using StadiumTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.VisitModels
{
    public class VisitListItem
    {
        public int VisitId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }

        public int ParkId { get; set; }
        public int VisitorId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }
        public virtual Park Park { get; set; }
        public virtual Visitor Visitor { get; set; }

        public bool GotPin { get; set; }
        public bool GotPhoto { get; set; }
    }
}
