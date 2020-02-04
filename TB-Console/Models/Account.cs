using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TripBlazrConsole.Models.Data;
using TripBlazrConsole.Models.ViewModels;

namespace TripBlazrConsole.Models
{
    public class Account
    {
       
        [Key]
        public int AccountId { get; set; }
        
        public string Name { get; set; }

        public string City { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public bool? Inactive { get; set; } 

        public bool? IsDeleted { get; set; }

        [Required]
        public string CitySlug { get; set; }

        public virtual ICollection<AccountUser> AccountUsers { get; set; }

        public virtual ICollection<Location> Locations { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<MenuGroup> MenuGroups { get; set; }
    }
}
