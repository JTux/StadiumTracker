using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Models.VisitorModels
{
    public class VisitorCreate
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { fullName = $"{ FirstName} {LastName}"; }
        }
    }
}