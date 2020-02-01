using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripBlazrConsole.Models
{
    public class Location
    {
        //public Location()
        //{
        //    this.Tags = new HashSet<Tag>();
        //    this.Categories = new HashSet<Category>();
        //}
        [Key]
        public int LocationId { get; set; }

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

        public bool? Inactive { get; set; } 

        public bool? IsDeleted { get; set; } 


        //address

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public int? Zipcode { get; set; }

        //hours

        public bool? Is24Hours { get; set; }

        public bool? SeeWebsite { get; set; } 

        public string HoursNotes { get; set; }
        
        public int? MondayOpen { get; set; }

        public int? MondayClose { get; set; }

        public int? TuesdayOpen { get; set; }

        public int? TuesdayClose { get; set; }

        public int? WednesdayOpen { get; set; }

        public int? WednesdayClose { get; set; }

        public int? ThursdayOpen { get; set; }

        public int? ThursdayClose { get; set; }

        public int? FridayOpen { get; set; }

        public int? FridayClose { get; set; }

        public int? SaturdayOpen { get; set; }

        public int? SaturdayClose { get; set; }

        public int? SundayOpen { get; set; }

        public int? SundayClose { get; set; }

        public virtual ICollection<LocationTag> LocationTags { get; set; }

        public virtual ICollection<LocationCategory> LocationCategories { get; set; }
    }
}

