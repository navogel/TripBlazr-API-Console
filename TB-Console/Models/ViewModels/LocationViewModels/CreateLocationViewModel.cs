using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models.ViewModels.LocationViewModels
{
    public class CreateLocationViewModel
    {
        public IFormFile File { get; set; }
        
       // public int LocationId { get; set; }

        [Required]

        public int AccountId { get; set; }

        [Required]
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Website { get; set; }

        public string ShortSummary { get; set; }

        public string Description { get; set; }

        //mapping


        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public int? SortId { get; set; }

        //media

        public string VideoId { get; set; }

        public int? VideoStartTime { get; set; }

        public int? VideoEndTime { get; set; }

        public string ImageUrl { get; set; }

        //City Controls 

        //public bool? Inactive { get; set; }

        //public bool? IsDeleted { get; set; }


        //address

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int? Zipcode { get; set; }

        //hours

        

        public bool? SeeWebsite { get; set; }

        public string HoursNotes { get; set; }

        public bool? IsActive { get; set; }
    }
}
