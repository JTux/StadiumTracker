using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Data
{
    public class Park
    {
        [Key]
        public int ParkId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string ParkName { get; set; }

        [Required]
        public string CityName { get; set; }

        public bool HasPin
        {
            get
            {
                return Visits.FirstOrDefault(v => v.GotPin) != null;
            }
        }

        public int PhotoCount
        {
            get
            {
                return Visits.Where(v => v.GotPhoto).Count();
            }
        }

        public virtual List<Visit> Visits { get; set; } = new List<Visit>();
    }
}