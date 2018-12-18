using StadiumTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.VisitModels
{
    public class VisitEdit
    {
        public int VisitId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }

        public int ParkId { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int VisitorId { get; set; }

        public bool GotPin { get; set; }
        public bool GotPhoto { get; set; }
        public string VisitorLastName { get; set; }
        public string VisitorFirstName { get; set; }
        public string AwayTeamName { get; set; }
        public string HomeTeamName { get; set; }
        public string ParkName { get; set; }
    }
}
