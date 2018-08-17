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
        public Visitor() { }

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

        //private string fullName;
        //public string FullName
        //{
        //    get { return fullName; }
        //    set { fullName = $"{ FirstName} {LastName}"; }
        //}

        public int TotalVisits { get; set; }
    }
}