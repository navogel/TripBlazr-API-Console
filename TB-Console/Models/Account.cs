using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class Account
    {
        [Required]
        public int AccountId { get; set; }
        
        public string Name { get; set; }

        public string City { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public string CitySlug { get; set; }
    }
}
