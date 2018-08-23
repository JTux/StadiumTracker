using StadiumTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.TeamModels
{
    public class TeamCreate
    {
        [Required]
        public string TeamName { get; set; }

        [Required]
        public int MyProperty { get; set; }

        public League League { get; set; }
    }
}
