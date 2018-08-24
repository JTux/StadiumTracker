﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Data
{
    public class Visitor
    {
        public Visitor() { }
        //TODO: Remove unused code.
        public Visitor(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            FullName = $"{FirstName} {LastName}";
        }

        [Key]
        public int VisitorId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName { get; set; }

        public int TotalVisits { get; set; }

        public int TotalPins { get; set; }
    }
}