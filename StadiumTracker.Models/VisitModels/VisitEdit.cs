using StadiumTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.VisitModels
{
    public class VisitEdit
    {
        public int VisitId { get; set; }
        public DateTime VisitDate { get; set; }

        public Park Park { get; set; }
        public Visitor Visitor { get; set; }
        //public virtual List<Visitor> VisitorList { get; set; }

        public bool GotPin { get; set; }
        public bool GotPhoto { get; set; }
    }
}
