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
        public Visit() { }

        public Visit(Park park, bool hasPin, bool hasPhoto, DateTime visitDate, List<Visitor> visitorList)
        {
            Park = park;
            HasPin = hasPin;
            HasPhoto = hasPhoto;
            VisitDate = visitDate;
            VisitorList = visitorList;
        }

        [Key]
        public int VisitID { get; set; }

        [Required]
        public Park Park { get; set; }
        
        [Required]
        public bool HasPin { get; set; }

        [Required]
        public bool HasPhoto { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        public List<Visitor> VisitorList { get; set; }
    }
}
