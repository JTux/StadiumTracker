using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Data
{
    public class Visitor
    {
        [Key]
        public int VisitorId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int TotalVisits
        {
            get
            {
                return Visits.Count;
            }
        }

        public int TotalPins
        {
            get
            {
                return Visits.Where(v => v.GotPin).Count();
            }
        }

        public virtual List<Visit> Visits { get; set; } = new List<Visit>();
    }
}