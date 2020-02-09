using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class Location
    {
       
        [Key]
        public int LocationId { get; set; }

        [Required]

        [JsonIgnore]
        public int AccountId { get; set; }

        [JsonIgnore]

        public Account Account { get; set; }

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

        [NotMapped]
        public IFormFile File { get; set; }

        //City Controls 

        public bool? IsActive { get; set; } 

        public bool? IsDeleted { get; set; } 


        //address

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int? Zipcode { get; set; }

        //hours

        public bool? SeeWebsite { get; set; } 

        public string HoursNotes { get; set; }


        [JsonIgnore]
        public virtual ICollection<LocationTag> LocationTags { get; set; }

       [JsonIgnore]
        public virtual ICollection<LocationCategory> LocationCategories { get; set; }

        [JsonIgnore]
        public virtual ICollection<Hours> Hours { get; set; }
    }
}

