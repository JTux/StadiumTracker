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

        [DefaultValue(false)]
        public bool IsVisited { get; set; }

        [DefaultValue(false)]
        public bool HasPin { get; set; }

        [DefaultValue(false)]
        public bool HasPhoto { get; set; }

        public int VisitCount { get; set; }
        public int PinCount { get; set; }
        public int PhotoCount { get; set; }
    }
}