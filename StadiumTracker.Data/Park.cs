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
        public Park() { }

        public Park(string name, string teamName)
        {
            Name = name;
            TeamName = teamName;
        }

        [Key]
        public int ParkID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string TeamName { get; set; }

        [DefaultValue(false)]
        public bool IsVisited { get; set; }
    }
}
