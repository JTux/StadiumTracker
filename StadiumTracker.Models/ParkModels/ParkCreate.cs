using StadiumTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.ParkModels
{
    public class ParkCreate
    {
        [Required]
        public string ParkName { get; set; }

        [Required]
        public int TeamId { get; set; }

        public virtual Team HomeTeam { get; set; }

        [Required]
        public string CityName { get; set; }
    }
}
