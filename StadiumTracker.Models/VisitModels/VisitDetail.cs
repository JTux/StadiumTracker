﻿using StadiumTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.VisitModels
{
    public class VisitDetail
    {
        public int VisitId { get; set; }
        public DateTime VisitDate { get; set; }

        public virtual Park Park { get; set; }
        public virtual Visitor Visitor { get; set; }
        //public List<Visitor> VisitorList { get; set; }

        public bool GotPin { get; set; }
        public bool GotPhoto { get; set; }
    }
}