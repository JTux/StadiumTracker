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

        public Visit(Park park, bool hasPin, bool hasPhoto, DateTime visitDate, Visitor visitor)
        {
            VisitDate = visitDate;
            Park = park;
            Visitor = visitor;
            GotPin = hasPin;
            GotPhoto = hasPhoto;
        }

        [Key]
        public int VisitId { get; set; }

        [Required]
        public int ParkId { get; set; }

        [Required]
        public int VisitorId { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        public virtual Park Park { get; set; }
        public virtual Visitor Visitor { get; set; }

        [Required]
        public bool GotPin { get; set; }

        [Required]
        public bool GotPhoto { get; set; }
    }
}