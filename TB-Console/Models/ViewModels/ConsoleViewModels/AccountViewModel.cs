using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TripBlazrConsole.Models.Data;

namespace TripBlazrConsole.Models.ViewModels.ConsoleViewModels
{
    public class AccountViewModel
    {
       
        public int AccountId { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

       
        public double Latitude { get; set; }

       
        public double Longitude { get; set; }

        //public bool? Inactive { get; set; }

        //public bool? IsDeleted { get; set; }

        //[Required]
        //public string CitySlug { get; set; }

        //public List<ApplicationUser> Users { get; set; }

        //public List<Location> Locations { get; set; }

        //public List<MenuTagsDetailsViewModel> MenuGroups { get; set; }
    }
}
