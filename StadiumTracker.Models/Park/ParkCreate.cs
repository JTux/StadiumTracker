﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.Park
{
    public class ParkCreate
    {
        [Required]
        public string ParkName { get; set; }

        [Required]
        public string TeamName { get; set; }
    }
}
