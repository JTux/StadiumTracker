using StadiumTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.VisitModels
{
    public class VisitDetail
    {
        public int VisitId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Park Park { get; set; }
        public Visitor Visitor { get; set; }

        public bool GotPin { get; set; }
        public bool GotPhoto { get; set; }
    }
}
